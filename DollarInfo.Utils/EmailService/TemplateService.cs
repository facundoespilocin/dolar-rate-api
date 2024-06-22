namespace DollarInfo.Utils.EmailService
{
    using System.IO;
    using System.Threading.Tasks;

    public class TemplateService
    {
        private readonly string _templatePath;

        public TemplateService(string templatePath)
        {
            _templatePath = templatePath;
        }

        public async Task<string> GetTemplateAsync(string templateName)
        {
            var filePath = Path.Combine(_templatePath, templateName);
            return await File.ReadAllTextAsync(filePath, System.Text.Encoding.UTF8);
        }
    }
}
