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
        /// 配置相应的服务和程序集
		/// </summary>
		protected override void Configure()
		{
            //AggregateCatalog 向其基础目录添加的目录会自动重新组合
            //筛选加载的集合中的IEnumerable<ComposablePartCatalog>
            var catalog = new AggregateCatalog(
				AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()
				);

            //AggregateCatalog 对象的基础目录中添加目录=》外部导入的dill或部件
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
            ////疑惑-->下面2句有什么用？不加也正常运行
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

        //获取相应要加载的程序集合，添加到可观察集合中
		protected override IEnumerable<Assembly> SelectAssemblies()
		{
			return new Assembly[] { Assembly.GetEntryAssembly(), typeof(IPart).Assembly };
		}

        //启动时配置，父类的未用到
		protected override void StartRuntime()
		{
            //指定调度模式，属性改变等在UI进行
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

        //设计时支持
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

        //获取具有指定的协定名称的第一个导出对象
        protected override object GetInstance(Type serviceType, string key)
		{
			string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
			var exports = container.GetExportedValues<object>(contract);

			if (exports.Count() > 0)
				return exports.First();

			throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
		}

        //供用户使用》获取具有指定的协定名称的所有已导出对象
        protected override IEnumerable<object> GetAllInstances(Type serviceType)
		{
			return container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
		}

        //暂时未用到
		protected override void BuildUp(object instance)
		{
			container.SatisfyImportsOnce(instance);
		}

        //退出保存开启配置服务，保存配置
		protected override void OnExit(object sender, EventArgs e)
		{
			base.OnExit(sender, e);

			IConfigurationService configService = IoC.Get<IConfigurationService>();
			configService.Save();
		}
	}
}
