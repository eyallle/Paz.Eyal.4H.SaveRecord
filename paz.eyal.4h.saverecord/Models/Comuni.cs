using System.Linq;
using System.IO.Enumeration;
using System.IO;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;

namespace paz.eyal._4h.saverecord.Models
{
 
    public class Comune

    {
        public int ID { get; set; }
        public string NomeComune { get; set; }
        public string CodiceCastale { get; set; }


        public Comune() { }

        public Comune(string riga, int id)
        {
            string[] colonne = riga.Split(',');
            ID = id;
            CodiceCastale = colonne[0];
            NomeComune = colonne[1];



        }
        public override string ToString()
        {
            return $"{ID}, {CodiceCastale}, {NomeComune}";
        }
    }


    public class Comuni : List<Comune> 
    {
        public string NomeFile { get; }
        public Comuni() { }

        public Comuni(string fileName)
        {
            NomeFile = fileName;

            using (FileStream fin = new FileStream(fileName, FileMode.Open))
            {
                StreamReader reader = new StreamReader(fin);
                int id = 1;

                while (!reader.EndOfStream)
                {
                    string riga = reader.ReadLine();
                    Comune c = new Comune(riga, id++);
                    Add(c);

                }
            }
        }
        public void Save()
        {
            string fn = NomeFile.Split(".")[0] + ".bin";
            Save(fn);
        }
        public void Save(string fileName)
        {


            FileStream fout = new FileStream(fileName, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(fout);

            foreach (Comune comune in this)
            {
                writer.Write(comune.ID);
                writer.Write(comune.CodiceCastale);
                writer.Write(comune.NomeComune);
            }
            writer.Flush();
            writer.Close();
        }
        public void Load()
        {
            string fn = NomeFile.Split(".")[0] + ".bin";
            Load(fn);
        }
        public void Load(string fileName)
        {
            Clear();

            FileStream fin = new FileStream(fileName, FileMode.Open);
            BinaryReader reader = new BinaryReader(fin);

            Comune c = new Comune();
         
            c.ID = reader.ReadInt32();

            c.CodiceCastale = reader.ReadString();

            c.NomeComune = reader.ReadString();

            Add(c);

            c.ID = reader.ReadInt32();
            c.CodiceCastale = reader.ReadString();
            c.NomeComune = reader.ReadString();
            Add(c);
        }

        public Comune RicercaComune(int index)
        {
            string fn = NomeFile.Split('.')[0] + ".bin";
            return RicercaComune(index, fn);
        }
       public Comune RicercaComune(int index, string fileName)
        {
            FileStream fin = new FileStream(fileName, FileMode.Open);
            BinaryReader reader = new BinaryReader(fin);

            fin.Seek((index - 1) * 32, SeekOrigin.Begin);
            Comune c = new Comune();
            c.ID = reader.ReadInt32();
            c.CodiceCastale = reader.ReadString();
            c.NomeComune = reader.ReadString();

            return c;
        }

    }

}