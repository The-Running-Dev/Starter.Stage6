using System.Data;

using NUnit.Framework;
using Microsoft.SqlServer.Server;
using Microsoft.Extensions.DependencyInjection;

using Starter.Bootstrapper;

namespace Starter.Framework.Tests
{
    /// <summary>
    /// Base class for the Starter.Framework.Tests project
    /// </summary>
    public class TestsBase
    {
        protected SqlDataRecord SqlDataRecord { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var serviceCollation = new ServiceCollection();
            Setup.Bootstrap(serviceCollation, SetupType.Test);

            SqlDataRecord = new SqlDataRecord(
                new SqlMetaData("Id", SqlDbType.UniqueIdentifier),
                new SqlMetaData("Name", SqlDbType.NVarChar, 20));
        }
    }

    public enum TestEnum
    {
        [System.ComponentModel.Description("First Item")]
        FirstItem,
        SecondItem
    }
}