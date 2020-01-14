namespace AC450Communication
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    public class DbData : INotifyPropertyChanged
    {
        public DbData(string name,string type, string net, string node, string ident, string user, string source, string file)
        {
            this.Name = name;
            this.Type = type;
            this.Net = net;
            this.Node = node;
            this.Ident = ident;
            this.User = user;
            this.Source = source;
            this.File = file;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { get; }

        public string Type { get; }

        public string Net { get; }

        public string Node { get; }

        public string Ident { get; }

        public string User { get; }

        public string Source { get; }

        public string File { get; }

        public static bool TryParse(string file, out IReadOnlyList<DbData> dbdata)
        {
            var filename = string.Empty;
            var net = string.Empty;
            var node = string.Empty;
            var type = string.Empty;
            var name = string.Empty;
            var ident = string.Empty;
            var user = string.Empty;
            var source = string.Empty;

            var foundDs = false;
            var foundTs = false;

            var results = new List<DbData>();

            foreach (var line in System.IO.File.ReadAllLines(file))
            {
                var tmpLine = line.Trim();

                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex("[ ]{2,}", options);
                tmpLine = regex.Replace(tmpLine, " ");
                
                var match = Regex.Match(tmpLine, "DS[1-9]", RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    filename = file;
                    type = "DS";
                    foundDs = true;
                }
                match = Regex.Match(tmpLine, "TS[1-9]",RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    filename = file;
                    type = "TS";
                    foundTs = true;
                }
                if (foundDs)
                {
                    if (tmpLine.ToUpper().Contains(":NAME"))
                    {
                        var array = tmpLine.Split(' ');
                        name = array[1];
                    }
                    if (tmpLine.ToUpper().Contains(":IDENT"))
                    {
                        var array = tmpLine.Split(' ');
                        ident = array[1];
                    }
                    if (tmpLine.ToUpper().Contains(":USER"))
                    {
                        var array = tmpLine.Split(' ');
                        user = array[1];
                    }
                    if (tmpLine.ToUpper().Contains(":SOURCE"))
                    {
                        var array = tmpLine.Split(' ');
                        source = array[1];
                    }
                    if (tmpLine.ToUpper().Contains(":NET"))
                    {
                        var array = tmpLine.Split(' ');
                        net = array[1];
                    }
                    if (tmpLine.ToUpper().Contains(":NODE"))
                    {
                        var array = tmpLine.Split(' ');
                        node = array[1];

                        results.Add(new DbData(name, type, net, node, ident, user, source, filename));

                        foundDs = false;
                    }


                }
                if (foundTs)
                {
                    if (tmpLine.ToUpper().Contains(":NAME"))
                    {
                        var array = tmpLine.Split(' ');
                        name = array[1];
                    }
                    if (tmpLine.ToUpper().Contains(":IDENT"))
                    {
                        var array = tmpLine.Split(' ');
                        ident = array[1];
                    }
                    if (tmpLine.ToUpper().Contains(":USER"))
                    {
                        var array = tmpLine.Split(' ');
                        user = array[1];
                    }
                    if (tmpLine.ToUpper().Contains(":SOURCE"))
                    {
                        var array = tmpLine.Split(' ');
                        source = array[1];
                    }
                    if (tmpLine.ToUpper().Contains(":NET"))
                    {
                        var array = tmpLine.Split(' ');
                        net = array[1];
                    }
                    if (tmpLine.ToUpper().Contains(":NODE"))
                    {
                        var array = tmpLine.Split(' ');
                        node = array[1];

                        results.Add(new DbData(name, type, net, node, ident, user, source, filename));

                        foundTs = false;
                    }
                }
                if (tmpLine == string.Empty)
                {
                    filename = string.Empty;
                    net = string.Empty;
                    node = string.Empty;
                    type = string.Empty;
                    name = string.Empty;
                    ident = string.Empty;
                    user = string.Empty;
                    source = string.Empty;

                    foundDs = false;
                    foundTs = false;
                }
            }
            if (results.Count > 0)
            {
                dbdata = results;
                return true;
            }

            dbdata = null;
            return false;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
