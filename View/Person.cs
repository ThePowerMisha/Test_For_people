using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.View {
    class Person {
        public Person(string lastName, string firstName, string secondName, string group, List<PersonData> data) {
            this.lastName = lastName;
            this.firstName = firstName;
            this.secondName = secondName;
            this.group = group;
            this.data = data;
        }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string secondName { get; set; }
        public string group { get; set; }
        public List<PersonData> data{get; set;}
    }

    class PersonData {
        public PersonData(int variant, int score) {
            this.variant = variant;
            this.score = score;
        }
        public int variant { get; set; }
        public int score { get; set; }
    }
}
