using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.View {
    class Card {
        public Card(string title, string path) {
            this.title = title;
            this.path = path;
        }
        public string title;
        public string path;
    }
}
