using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows.Controls;
using static WpfApp1.View.mainPage;

namespace dBController {
    public class results {
        public JArray ID { get; set; }
        public JArray lastName { get; set; }
        public JArray firstName { get; set; }
        public JArray secondName { get; set; }
        public JArray group { get; set; }
        public JArray theme { get; set; }
        public JArray block { get; set; }
        public JArray load { get; set; }
        public JArray variant { get; set; }
        public JArray timeSpent { get; set; }
        public JArray tips { get; set; }
        public JArray scoreResult { get; set; }
        public JArray correctAnswers { get; set; }
        public JArray testDate { get; set; }

        public static results createEntery() {
            using (StreamReader file = new StreamReader("Database/Results_Test.json", System.Text.Encoding.UTF8)) {
                JsonTextReader reader = new JsonTextReader(file);
                JObject obj_students = (JObject)JToken.ReadFrom(reader);
                JArray arr_ID = obj_students.GetValue("ID") as JArray;
                JArray arr_lastName = obj_students.GetValue("lastName") as JArray;
                JArray arr_firstName = obj_students.GetValue("firstName") as JArray;
                JArray arr_secondName = obj_students.GetValue("secondName") as JArray;
                JArray arr_group = obj_students.GetValue("group") as JArray;
                JArray arr_theme = obj_students.GetValue("theme") as JArray;
                JArray arr_block = obj_students.GetValue("block") as JArray;
                JArray arr_load = obj_students.GetValue("load") as JArray;
                JArray arr_variant = obj_students.GetValue("variant") as JArray;
                JArray arr_timeSpent = obj_students.GetValue("timeSpent") as JArray;
                JArray arr_tips = obj_students.GetValue("tips") as JArray;
                JArray arr_scoreResult = obj_students.GetValue("scoreResult") as JArray;
                JArray arr_correctAnswers = obj_students.GetValue("correctAnswers") as JArray;
                JArray arr_testDate = obj_students.GetValue("testDate") as JArray;

                results r = new results {
                    ID = arr_ID,
                    lastName = arr_lastName,
                    firstName = arr_firstName,
                    secondName = arr_secondName,
                    group = arr_group,
                    theme = arr_theme,
                    block = arr_block,
                    load = arr_load,
                    variant = arr_variant,
                    timeSpent = arr_timeSpent,
                    tips = arr_tips,
                    scoreResult = arr_scoreResult,
                    correctAnswers = arr_correctAnswers,
                    testDate = arr_testDate

                };
                return r;
            }
        }

        // Загрузка в Results.json
        public static void saveData(results r) {
            using (StreamWriter file = File.CreateText("Database/Results_Test.json")) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, r);
            }
        }

        // "Генерация" ID
        public static int idGeneration(results r, int id = -1) {
            Random rnd = new Random();
            // Провреятся если такой пользователь
            for(int i = 0; i < r.ID.Count(); i++) {
                if (r.lastName[i].ToString() == r.lastName[r.lastName.Count() - 1].ToString() && 
                    r.firstName[i].ToString() == r.firstName[r.firstName.Count() - 1].ToString() && 
                    r.secondName[i].ToString() == r.secondName[r.secondName.Count() - 1].ToString() &&
                    r.group[i].ToString() == r.group[r.group.Count() - 1].ToString()) {
                    id = System.Convert.ToInt32(r.ID[i]);
                }
            }
            // Если нет, то генерируется случайное ID
            if (id == -1) {
                restart:
                id = rnd.Next(100000, 999999);

                // Проверка на наличие такого же ID
                foreach (int item in r.ID) {
                    if (id == item) {
                        goto restart;  // Если ID слуйчано уже получилось такое, какое есть генерируется новое и проврка начинается заного
                    }
                }
            }
            return id;
        }

        public static Dictionary<Tuple<string, string, string, string>, List<List<string>>> getAllResults(results r) {
            // Возвращает полный список результатов вида
            /* 
            {

                ["Фамилия", "Имя", "Отчество", "Группа"]: [
                        ["Тема", "Блок", "Нагрузка", "Вариант", "Потраченное время", "Кол-во подсказок", "Баллы/Оценка", "Правильные ответы", "Дата"],
                        ["Тема", "Блок", "Нагрузка", "Вариант", "Потраченное время", "Кол-во подсказок", "Баллы/Оценка", "Правильные ответы", "Дата"],
                        ...
                      ],
                ["Фамилия", "Имя", "Отчество", "Группа"]: [
                        ["Тема", "Блок", "Нагрузка", "Вариант", "Потраченное время", "Кол-во подсказок", "Баллы/Оценка", "Правильные ответы", "Дата"],
                        ["Тема", "Блок", "Нагрузка", "Вариант", "Потраченное время", "Кол-во подсказок", "Баллы/Оценка", "Правильные ответы", "Дата"],
                        ...
                      ],
                ...
            } */

            Dictionary<Tuple<string, string, string, string>, List<List<string>>> allResults = new Dictionary<Tuple<string, string, string, string>, List<List<string>>>();
            List<string> explored = new List<string>();
            for (int i = 0; i < r.theme.Count; i++) {
                // Если ID еще не обрабатывалось, то начинаем работу с ним 
                if (!explored.Contains(r.ID[i].ToString())) {
                    var FIOG = new Tuple<string, string, string, string> // Записываем в отдельный список ФИО и группу
                    (
                        r.lastName[i].ToString(),
                        r.firstName[i].ToString(),
                        r.secondName[i].ToString(),
                        r.group[i].ToString()
                    );

                    var testData = new List<List<string>>(); // Инициализация списка, с данными о всех прохождениях студента
                    testData.Add
                        (new List<string> {
                            r.theme[i].ToString(),
                            r.block[i].ToString(),
                            r.load[i].ToString(),
                            r.variant[i].ToString(),
                            r.timeSpent[i].ToString(),
                            r.tips[i].ToString(),
                            r.scoreResult[i].ToString(),
                            r.correctAnswers[i].ToString(),
                            r.testDate[i].ToString()
                        }
                        );
                    allResults.Add(FIOG, testData); // Добавление данных в общий словарь
                    explored.Add(r.ID[i].ToString());
                }
                // Если ID уже был обработан хотя бы раз, то мы записываем данные о тестах в значения, используя как ключ пользовательские данные
                else {
                    var FIOG = new Tuple<string, string, string, string> // Записываем в отдельный список ФИО и группу
                    (
                        r.lastName[i].ToString(),
                        r.firstName[i].ToString(),
                        r.secondName[i].ToString(),
                        r.group[i].ToString()
                    );
                    var testData = new List<List<string>>(); // Инициализация списка, с данными о всех прохождениях студента
                    testData.Add                            
                        (new List<string>
                        {
                            r.theme[i].ToString(),
                            r.block[i].ToString(),
                            r.load[i].ToString(),
                            r.variant[i].ToString(),
                            r.timeSpent[i].ToString(),
                            r.tips[i].ToString(),
                            r.scoreResult[i].ToString(),
                            r.correctAnswers[i].ToString(),
                            r.testDate[i].ToString()
                        }
                        );
                    foreach(List<string> row in allResults[FIOG]) {
                        testData.Add(row);
                    }
                    allResults[FIOG] = testData;
                }
            }
            return allResults;
        }
    }

    public class questions {
        
        // Возвращает полную базу данных
        public static JObject getBase() {
            using (StreamReader file = new StreamReader("Database/Questions.json", System.Text.Encoding.UTF8)) {
                JsonTextReader reader = new JsonTextReader(file);
                JObject Questions = (JObject)JToken.ReadFrom(reader);
                return Questions;
            }
        }

        // Возвращает список имен всех тем
        public static List<string> getThemesName() {
            List<string> listThems = getBase().Properties().Select(p => p.Name).ToList();
            JObject quesBase = getBase();
            List<string> listThemsName = new List<string>();
            foreach(string item in listThems) {
                listThemsName.Add(quesBase.GetValue(item)["name"].ToString());
            }
            return listThemsName;
        }

        // Возращает имя темы для работы методов
        public static string getTrueName(string themeName) {
            JObject quesBase = getBase();
            List<string> listThems = quesBase.Properties().Select(p => p.Name).ToList();
            foreach (string item in listThems) {
                if (themeName == quesBase.GetValue(item)["name"].ToString()) {
                    themeName = item;
                }
            }
            return themeName;
        }

        // Возращает JToken конкретного блока
        public static JObject getBlock(string themeName, int blockID) {
            JObject quesBase = getBase();
            themeName = getTrueName(themeName);
            JToken blocks = quesBase.GetValue(themeName)["blocks"];
            JObject block = new JObject();
            foreach (JObject item in blocks) {
                if (item["blockID"].ToString() == blockID.ToString()) {
                    block = item;
                }
            }           
            return block;
        }

        // Возвращает JToken конкретной нагрузки
        public static JObject getLoad(string themeName, int blockID, int loadID) {
            JObject load = new JObject();
            themeName = getTrueName(themeName);
            JToken loads = getBlock(themeName, blockID)["loads"];
            foreach(JObject item in loads) {
                if(item["loadID"].ToString() == loadID.ToString()) {
                    load = item;
                }
            }
            return load;
        }

        // Возвращает массив пар элементов ввида [ID, путь к картинке] для всех блоков данной темы
        public static List<List<string>> getBlocksImgPath(string themeName) {
            themeName = getTrueName(themeName);
            JObject quesBase = getBase();
            List<List<string>> ListImgPath = new List<List<string>>();
            JArray arr_blocks = quesBase.GetValue(themeName)["blocks"] as JArray;
            foreach (JToken item in arr_blocks) {
                ListImgPath.Add(new List<string>() { item["blockID"].ToString(), item["pathToBlockImg"].ToString() });
            }
            return ListImgPath;
        }

        // Возвращает массив пар элементов ввида [ID, путь к картинке] для всех нагрузок конкретного блока
        public static List<List<string>> getLoadsImgPath(string themeName, int blockID) {
            List<List<string>> ListLoadsImgPath = new List<List<string>>();
            JObject block = getBlock(themeName, blockID);
            foreach (JToken item in block["loads"]) {
                ListLoadsImgPath.Add(new List<string>() { item["loadID"].ToString(), item["pathToLoadImg"].ToString() });
            }
            return ListLoadsImgPath;
        }

        // Возвращает массив пар элементов ввида [ID, путь к картинке] для всех вариантов конкретного блока и нагрузки
        public static List<List<string>> getVariantsImgPath(string themeName, int blockID, int loadID) {
            List<List<string>> ListVariantsImgPath = new List<List<string>>();
            themeName = getTrueName(themeName);
            JObject load = getLoad(themeName, blockID, loadID);
            foreach(JToken item in load["variants"]) {
                ListVariantsImgPath.Add(new List<string>() { item["variantID"].ToString(), item["pathToMainBlock"].ToString() });
            }
            return ListVariantsImgPath;
        }

        // Вовзращает JToken конкретного варианта
        public static JObject getVariant(string themeName, int blockID, int loadID, int variantID) {
            JObject variant = new JObject();
            themeName = getTrueName(themeName);
            JToken variants = getLoad(themeName, blockID, loadID)["variants"];
            foreach (JObject item in variants) {
                if (item["variantID"].ToString() == variantID.ToString()) {
                    variant = item;
                }
            }
            return variant;
        }

        // Возвращает словарь исходных данных вида переменная: значение
        public static Dictionary<string, double> getQuestionParams(string themeName, int blockID, int loadID, int variantID) {
            JObject variant = getVariant(getTrueName(themeName), blockID, loadID, variantID);
            Dictionary<string, double> par = new Dictionary<string, double>();
            foreach(JProperty item in variant["questionParams"]) {
                par.Add(item.Name, System.Convert.ToDouble(item.Value.ToString()));
            }
            return par;
        }

        // Возвращает описание того, что нужно найти на этапе stageNumber
        public static string getQuestionText(string themeName, int blockID, int loadID, int variantID, int stageNumber) {
            JObject variant = getVariant(getTrueName(themeName), blockID, loadID, variantID);
            string questionText = variant["questionText"][stageNumber - 1].ToString();
            return questionText;
        }

        // Возвращает список переменых, которые нужно найти в этапе stageNumber
        public static List<string> getQuestionFinds(string themeName, int blockID, int loadID, int variantID, int stageNumber) {
            List<string> questionFinds = new List<string>();
            JObject variant = getVariant(getTrueName(themeName), blockID, loadID, variantID);
            foreach (JToken item in variant["questionFind"][stageNumber - 1]) {
                questionFinds.Add(item.ToString());
            }
            return questionFinds;
        }

        // Возвращает максимальное число этапов вопросов
        public static int getMaxQuestionFinds(string themeName, int blockID, int loadID, int variantID) {
           int maxStageNumber = 0;
           JObject variant = getVariant(getTrueName(themeName), blockID, loadID, variantID);
           foreach (JToken item in variant["questionFind"]) {
               maxStageNumber++;
           }
           return maxStageNumber;
        }

        // Возвращает словарь вида 0/или найденная перменная: формула
        public static List<Dictionary<string, string>> getQuestionFormuls(string themeName, int blockID, int loadID, int variantID, int stageNumber) {
            List<Dictionary<string, string>> questionFormuls = new List<Dictionary<string, string>>();
            JObject variant = getVariant(getTrueName(themeName), blockID, loadID, variantID);
            foreach (JToken item in variant["questionFormula"][stageNumber - 1]) {
                Dictionary<string, string> par = new Dictionary<string, string>();
                foreach(JProperty item_ in item) {
                    par.Add(item_.Name, item_.Value.ToString() );
                }
                questionFormuls.Add(par);
            }
            return questionFormuls;
        }

        // Возвращает путь главного чертежа варианта
        public static string getMainVariantImgPath(string themeName, int blockID, int loadID, int variantID) {
            return getVariant(getTrueName(themeName), blockID, loadID, variantID)["pathToMainBlock"].ToString();
        }

        // Возвращает список путей картинок подсказок
        public static List<string> getTipsPath(string themeName, int blockID, int loadID, int variantID) {
            List<string> tipsPath = new List<string>();
            JObject variant = getVariant(getTrueName(themeName), blockID, loadID, variantID);
            foreach(var item in variant["tips"]) {
                tipsPath.Add(item.ToString());
            }
            return tipsPath;
        }
    }
}
