using FluentMigrator;
using Nop.Data.Migrations;

namespace Ts.Plugin.Misc.Membership.Data
{
    [SkipMigrationOnUpdate]
    [NopMigration("2021/03/22 20:32:55:1687541", "Misc.Membership base schema")]
    public class SchemaMigration : AutoReversingMigration
    {
        protected IMigrationManager _migrationManager;

        public SchemaMigration(IMigrationManager migrationManager)
        {
            _migrationManager = migrationManager;
        }

        public override void Up()
        {
            _migrationManager.BuildTable<Domain.Membership>(Create);
        }
    }
}
