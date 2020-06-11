using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MultipleDbContextEfCoreDemo.EntityFrameworkCoreSecond;

namespace MultipleDbContextEfCoreDemo.EntityFrameworkCore
{
    [DependsOn(
        typeof(MultipleDbContextEfCoreDemoCoreModule),
        typeof(MultipleDbContextEfCoreDemoEntityFrameworkCoreSecondModule),
        typeof(AbpEntityFrameworkCoreModule))]
    public class MultipleDbContextEfCoreDemoEntityFrameworkCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.ReplaceService<IConnectionStringResolver, MyConnectionStringResolver>();

            // Configure first DbContext
            Configuration.Modules.AbpEfCore().AddDbContext<MultipleDbContextEfCoreDemoDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    DbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    DbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                }
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MultipleDbContextEfCoreDemoEntityFrameworkCoreModule).GetAssembly());
        }
    }
}