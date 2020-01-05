using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using IWHR.DataAnalysisService;
//using IWHR.DataProcessService;
using Microsoft.Extensions.DependencyInjection;

namespace SNWDSafeMonitor
{ 
    public static class ServcieExtension
    {
        //public static IServiceCollection AddReduceService(this IServiceCollection service)
        //{
        //    return service.AddSingleton(factory => new MultiloggerTransService());
        //}
        //public static IServiceCollection AddMultiloggerService(this IServiceCollection service)
        //{
        //    return service.AddSingleton(factory => new MultiloggerTransService());
        //}
        //public static IServiceCollection AddErrorCheckService(this IServiceCollection service)
        //{
        //    return service.AddSingleton(factory => new ErrorCheckService());
        //}
        //public static IServiceCollection AddCurrentDataAnaService(this IServiceCollection service)
        //{
        //    return service.AddSingleton(factory => new CurrentDataAnalysisService());
        //}
        //public static IServiceCollection AddNewDataAnalysisService(this IServiceCollection service)
        //{
        //    return service.AddSingleton(factory => new NewDataAnalysisService());
        //}
        
        //public static IServiceCollection AddDataAnaService(this IServiceCollection service)
        //{
        //    return service.AddSingleton(factory => new DataAnalysisService());
        //}
        //public static IServiceCollection AddStatModelCalcService(this IServiceCollection service)
        //{
        //    return service.AddSingleton(factory => new StatModelCalcService());
        //}
        //public static IServiceCollection AddMySqlTransService(this IServiceCollection service)
        //{
        //    return service.AddSingleton(factory => new MySqlTransService());
        //}
        /*
         * 6.1 瞬时（Transient）

生命周期服务在它们每次请求时被创建。这一生命周期适合轻量级的，无状态的服务。

6.2 作用域（Scoped）

作用域生命周期服务在每次请求被创建一次。

6.3 单例（Singleton）

单例生命周期服务在它们第一次被请求时创建并且每个后续请求将使用相同的实例。
         */
    }
}
