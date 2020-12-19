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
                    correctAnswers = arr_correctAnswers,
                    testDate = arr_testDate

                };
                return r;
            }
        }

        public static void saveInput(results r, int id, string lastName, string firstName, string secondName, string group) {
            r.ID.Add(id);
            r.lastName.Add(lastName);
            r.firstName.Add(firstName);
            r.secondName.Add(secondName);
            r.group.Add(group);
            using (StreamWriter file = File.CreateText("Database/Results_Test.json")) {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, r);
            }
        }

        public static int idGeneration(results r) {
            Random rnd = new Random();
            int id = rnd.Next(100000, 999999);
            foreach (int item in r.ID) {
                if (id == item) {
                    id = idGeneration(r);
                }
            }
            return id;
        }
    }

    public class questions {
        public static JObject getBase() {
            using (StreamReader file = new StreamReader("Database/Questions.json", System.Text.Encoding.UTF8)) {
                JsonTextReader reader = new JsonTextReader(file);
                JObject Questions = (JObject)JToken.ReadFrom(reader);
                return Questions;
            }
        }

        public static List<string> getThemesName() {
            List<string> listThems = getBase().Properties().Select(p => p.Name).ToList();
            JObject quesBase = getBase();
            List<string> listThemsName = new List<string>();
            foreach (string item in listThems) {
                listThemsName.Add(quesBase.GetValue(item)["name"].ToString());
            }
            return listThemsName;
        }

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

        public static JObject getLoad(string themeName, int blockID, int loadID) {
            JObject load = new JObject();
            themeName = getTrueName(themeName);
            JToken loads = getBlock(themeName, blockID)["loads"];
            foreach (JObject item in loads) {
                if (item["loadID"].ToString() == loadID.ToString()) {
                    load = item;
                }
            }
            return load;
        }

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

        public static List<List<string>> getLoadsImgPath(string themeName, int blockID) {
            List<List<string>> ListLoadsImgPath = new List<List<string>>();
            JObject block = getBlock(themeName, blockID);
            foreach (JToken item in block["loads"]) {
                ListLoadsImgPath.Add(new List<string>() { item["loadID"].ToString(), item["pathToLoadImg"].ToString() });
            }
            return ListLoadsImgPath;
        }

        public static List<List<string>> getVariantsImgPath(string themeName, int blockID, int loadID) {
            List<List<string>> ListVariantsImgPath = new List<List<string>>();
            themeName = getTrueName(themeName);
            JObject load = getLoad(themeName, blockID, loadID);
            foreach (JToken item in load["variants"]) {
                ListVariantsImgPath.Add(new List<string>() { item["variantID"].ToString(), item["pathToMainBlock"].ToString() });
            }
            return ListVariantsImgPath;
        }

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

        public static Dictionary<string, double> getQuestionParams(string themeName, int blockID, int loadID, int variantID) {
            JObject variant = getVariant(getTrueName(themeName), blockID, loadID, variantID);
            Dictionary<string, double> par = new Dictionary<string, double>();
            foreach (JProperty item in variant["questionParams"]) {
                par.Add(item.Name, System.Convert.ToDouble(item.Value.ToString()));
            }
            return par;

        }

        public static string getQuestionText(string themeName, int blockID, int loadID, int variantID, int stageNumber) {
            JObject variant = getVariant(getTrueName(themeName), blockID, loadID, variantID);
            string questionText = variant["questionText"][stageNumber - 1].ToString();
            return questionText;
        }

        //public static int maxStageNumber = 0;

        public static int getMaxQuestionFinds(string themeName, int blockID, int loadID, int variantID)
        {
            int maxStageNumber = 0;
            JObject variant = getVariant(getTrueName(themeName), blockID, loadID, variantID);
            foreach (JToken item in variant["questionFind"])
            {
                maxStageNumber++;
            }
            return maxStageNumber;
        }


        public static List<string> getQuestionFinds(string themeName, int blockID, int loadID, int variantID, int stageNumber) {

            //maxStageNumber++;

            List<string> questionFinds = new List<string>();
            JObject variant = getVariant(getTrueName(themeName), blockID, loadID, variantID);
            foreach (JToken item in variant["questionFind"][stageNumber - 1]) {
                questionFinds.Add(item.ToString());
            }
            return questionFinds;
        }

        public static List<Dictionary<string, string>> getQuestionFormuls(string themeName, int blockID, int loadID, int variantID, int stageNumber) {
            List<Dictionary<string, string>> questionFormuls = new List<Dictionary<string, string>>();
            JObject variant = getVariant(getTrueName(themeName), blockID, loadID, variantID);
            foreach (JToken item in variant["questionFormula"][stageNumber - 1]) {
                
                Dictionary<string, string> par = new Dictionary<string, string>();

                foreach (JProperty item1 in item)
                {
                    par.Add(item1.Name, System.Convert.ToString(item1.Value));
                }
                questionFormuls.Add(par);
            }
            return questionFormuls;
        }
    }
}
