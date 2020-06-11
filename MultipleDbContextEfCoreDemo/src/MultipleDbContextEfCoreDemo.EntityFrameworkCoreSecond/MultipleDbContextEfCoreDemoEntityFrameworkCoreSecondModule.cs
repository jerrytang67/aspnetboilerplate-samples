using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MultipleDbContextEfCoreDemo.EntityFrameworkCore;
using MultipleDbContextEfCoreDemo.EntityFrameworkCoreSecond.EntityFrameworkCore;

namespace MultipleDbContextEfCoreDemo.EntityFrameworkCoreSecond
{
    [DependsOn(
        typeof(MultipleDbContextEfCoreDemoCoreModule),
        typeof(AbpEntityFrameworkCoreModule))]
    public class MultipleDbContextEfCoreDemoEntityFrameworkCoreSecondModule : AbpModule
    {
        public override void PreInitialize()
        {
            // Configure second DbContext
            Configuration.Modules.AbpEfCore().AddDbContext<MultipleDbContextEfCoreDemoSecondDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    SecondDbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    SecondDbContextOptionsConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                }
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MultipleDbContextEfCoreDemoEntityFrameworkCoreSecondModule).GetAssembly());
        }
    }
}