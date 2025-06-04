namespace Progress
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ConfigReader configReader = await ConfigReader.CreateAsync();

            var data = await configReader.Read();

            foreach (var item in data) 
            { 
                Console.WriteLine(item);
            }
        }
    }

    interface IProgressReporter
    {
        void Progress(int percent);
    }

    class ConfigReader : IProgressReporter
    {
        private readonly string weaponConfig = @"{
            'pistols': [
                {
                    'name': 'Glock-18',
                    'price': 300,
                    'damage': 28,
                    'ammo': 20,
                    'fire_rate': 0.15
                },
                {
                    'name': 'USP-S',
                    'price': 500,
                    'damage': 35,
                    'ammo': 12,
                    'fire_rate': 0.12
                }
            ],
            'rifles': [
                {
                    'name': 'AK-47',
                    'price': 2700,
                    'damage': 36,
                    'ammo': 30,
                    'fire_rate': 0.1
                },
                {
                    'name': 'M4A4',
                    'price': 3100,
                    'damage': 33,
                    'ammo': 30,
                    'fire_rate': 0.09
                }
            ]
        }";

        public readonly string path;
        private bool fileReady = false;

        private ConfigReader()
        {
            path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cfg.txt");
        }

        public static async Task<ConfigReader> CreateAsync()
        {
            var reader = new ConfigReader();
            await reader.ConfigWriter();
            return reader;
        }

        private async Task ConfigWriter()
        {
            if (!File.Exists(path))
            {
                await File.WriteAllTextAsync(path, weaponConfig);
            }
            fileReady = true;
        }

        public async Task<string[]> Read()
        {
            List <string> data = new List<string> ();
            if (!fileReady)
            {
                throw new InvalidOperationException("Файл не готов к чтению");
            }
            var lines = await File.ReadAllLinesAsync(path);
            for (int i = 0; i < lines.Length; i++)
            {
                data.Add(lines[i]);
                await Task.Delay(250);
                Progress(((i+1)*100)/lines.Length);
            }
            return data.ToArray();
        }

        public void Progress(int percent)
        {
            Console.WriteLine($"Прогресс: {percent}%");
        }
    }
}
