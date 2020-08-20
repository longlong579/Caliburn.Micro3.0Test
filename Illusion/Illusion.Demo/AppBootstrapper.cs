namespace Illusion.Demo
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.ComponentModel.Composition.Hosting;
	using System.ComponentModel.Composition.Primitives;
	using System.Linq;
	using Illusion;
	using System.Windows;
	using System.Reflection;
    using System.Collections.ObjectModel;
	using Caliburn.Micro;
	public class AppBootstrapper : Bootstrapper<IShell>
	{
		CompositionContainer container;

		/// <summary>
		/// By default, we are configured to use MEF
        /// ������Ӧ�ķ���ͳ���
		/// </summary>
		protected override void Configure()
		{
            //AggregateCatalog �������Ŀ¼��ӵ�Ŀ¼���Զ��������
            //ɸѡ���صļ����е�IEnumerable<ComposablePartCatalog>
            var catalog = new AggregateCatalog(
				AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()
				);

            //AggregateCatalog ����Ļ���Ŀ¼�����Ŀ¼=���ⲿ�����dill�򲿼�
            catalog.Catalogs.Add(new DirectoryCatalog(Environment.CurrentDirectory + "/AddIns"));

			container = new CompositionContainer(catalog);

			var batch = new CompositionBatch();

			var windowManager = new AvalonDockWindowManager();

			batch.AddExportedValue<IEventAggregator>(new EventAggregator());
			batch.AddExportedValue<IResourceService>(new ResourceService());
			batch.AddExportedValue<IWindowManager>(windowManager);
			batch.AddExportedValue<IDockWindowManager>(windowManager);
			batch.AddExportedValue<IConfigurationService>(new ConfigurationService(@"Configuration"));
			batch.AddExportedValue<IThemeService>(new ThemeService());
			batch.AddExportedValue<IOptionService>(new OptionService());
            ////�ɻ�-->����2����ʲô�ã�����Ҳ��������
            //batch.AddExportedValue(container);
            //batch.AddExportedValue(catalog);

            container.Compose(batch);

            //ComposablePartCatalog aa=container.Catalog;
            //ReadOnlyCollection<ExportProvider> providers = container.Providers;

            container.ComposeParts(container.GetExportedValue<IResourceService>());
			container.ComposeParts(container.GetExportedValue<IThemeService>());
			container.ComposeParts(container.GetExportedValue<IOptionService>());

			//Extend the name rule of ViewLocator, the rule is : LanguageOption -> LanaguageOptionView
			ViewLocator.NameTransformer.AddRule("Option$", "OptionView");
		}

        //��ȡ��ӦҪ���صĳ��򼯺ϣ���ӵ��ɹ۲켯����
		protected override IEnumerable<Assembly> SelectAssemblies()
		{
			return new Assembly[] { Assembly.GetEntryAssembly(), typeof(IPart).Assembly };
		}

        //����ʱ���ã������δ�õ�
		protected override void StartRuntime()
		{
            //ָ������ģʽ�����Ըı����UI����
			Execute.InitializeWithDispatcher();
			AssemblySource.Instance.AddRange(SelectAssemblies());

			Application = Application.Current;
			PrepareApplication();

			//Reorder to put configure at the last step.
			IoC.GetInstance = GetInstance;
			IoC.GetAllInstances = GetAllInstances;
			IoC.BuildUp = BuildUp;
			Configure();
        }

        //���ʱ֧��
		protected override void StartDesignTime()
		{
			//Init ResourceService for design time.
			ResourceService resource = new ResourceService();
			resource.Resources = new IResource[] { new Resource() };
			IoC.GetInstance = (i, u) =>
				{
					if (i == typeof(IResourceService))
					{
						return resource;
					}
					return null;
				};
		}

        //��ȡ����ָ����Э�����Ƶĵ�һ����������
        protected override object GetInstance(Type serviceType, string key)
		{
			string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
			var exports = container.GetExportedValues<object>(contract);

			if (exports.Count() > 0)
				return exports.First();

			throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
		}

        //���û�ʹ�á���ȡ����ָ����Э�����Ƶ������ѵ�������
        protected override IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
		}

        //��ʱδ�õ�
		protected override void BuildUp(object instance)
		{
			container.SatisfyImportsOnce(instance);
		}

        //�˳����濪�����÷��񣬱�������
		protected override void OnExit(object sender, EventArgs e)
		{
			base.OnExit(sender, e);

			IConfigurationService configService = IoC.Get<IConfigurationService>();
			configService.Save();
		}
	}
}
