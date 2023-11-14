using Newtonsoft.Json;
using System.Xml;
using System.Xml.Serialization;

namespace converter
{
    class Prgoram
    {
        static objects tmp;
        private static void Main()
        {
            convert.is_not_exists();
            while (true)
            {
                tmp = new objects();
                Console.Clear();
                Console.WriteLine("Введите путь:");
                get_path(Console.ReadLine());
            }
        }
        private static void get_path(string path)
        {
            Console.Clear();
            int get_int = 0;
            if (path == null) return;
            else if (path == "object.txt")
            {
                tmp = convert.Convert_from_txt();
                tmp.info();
            }
            else if (path == "object.json")
            {
                tmp = convert.Convert_from_json();
                tmp.info();
            }
            else if (path == "object.xml")
            {
                tmp = convert.Convert_from_xml();
                tmp.info();
            }
            else
            {
                Console.WriteLine("Ошибочка в пути");
                return;
            }
            Console.WriteLine("Выберите во что перевести:\n1. в Json\n2. в XML\n3.в TXT");
            try
            {
                get_int = Convert.ToInt32(Console.ReadLine());

            }
            catch { Console.WriteLine("Произошла ошибка"); return; }
            switch (get_int)
            {
                case 1:
                    convert.Convert_to_json(tmp); break;
                case 2:
                    convert.Convert_to_xml(tmp); break;
                case 3:
                    convert.Convert_to_txt(tmp); break;
                default:
                    Console.WriteLine("Это че такое");
                    return;
            }
            Console.WriteLine("Вроде перевел, чекай");
            Thread.Sleep(1000);
        }
    }
    public class objects
    {
        public string properties_1;
        public string properties_2;

        public void info() => Console.WriteLine(properties_1 + '\n' + properties_2);
    }
    public class convert
    {
        public static void is_not_exists()
        {
            if (!File.Exists("object.json"))
                File.WriteAllText("object.json", "");
            if (!File.Exists("object.txt"))
                File.WriteAllText("object.txt", "Свойство1:Свойство2");
            if (!File.Exists("object.xml"))
                File.WriteAllText("object.xml", "");
        }
        public static void Convert_to_json(objects obj) => File.WriteAllText("object.json", JsonConvert.SerializeObject(obj));
        public static objects Convert_from_json() => JsonConvert.DeserializeObject<objects>(File.ReadAllText("object.json"));
        public static void Convert_to_txt(objects obj) => File.WriteAllText("object.txt", $"{obj.properties_1}:{obj.properties_2}");
        public static objects Convert_from_txt()
        {
            objects new_obj = new objects();
            List<string> tmp = File.ReadAllText("object.txt").Split(':').ToList();
            new_obj.properties_1 = tmp[0];
            new_obj.properties_2 = tmp[1];
            return new_obj;
        }
        public static void Convert_to_xml(object obj)
        {
            XmlSerializer xml = new XmlSerializer(typeof(objects));
            using (FileStream fs = new FileStream("object.xml", FileMode.OpenOrCreate))
                xml.Serialize(fs, obj);
        }
        public static objects Convert_from_xml()
        {
            objects obj;
            XmlSerializer xml = new XmlSerializer(typeof(objects));
            using (FileStream fs = new FileStream("object.xml", FileMode.Open))
                obj = (objects)xml.Deserialize(fs);
            return obj;
        }
    }
}