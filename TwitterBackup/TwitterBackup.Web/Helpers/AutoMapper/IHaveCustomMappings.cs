using AutoMapper;

namespace TwitterBackup.Web.Helpers.AutoMapper
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration configuration);
    }
}