using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public class Note
{
    public int id { get; set; }
    public string date { get; set; }
    public string time { get; set; }
    public string subject { get; set; }
    public string text { get; set; }
    public string telephone { get; set; }
}


public class Notepad
{
    public List<Note> Notes { get; set; } = new List<Note>();
}

class Program
{
    static void Main(string[] args)
    {
        string xml = @"<?xml version=""1.0"" ?>
                        <notepad>
                            <note id=""1"" date=""12/04/99"" time=""13:40"">
                                <subject>Важлива ділова зустріч</subject>
                                <importance />
                                <text>
                                    Треба зустрітися з Іваном Івановичем, попередньо зателефонувавши йому за телефоном <tel>123-12-12</tel>
                                </text>
                            </note>
                            <note id=""2"" date=""12/04/99"" time=""13:58"">
                                <subject>Зателефонувати додому</subject>
                                <text>
                                    <tel>124-13-13</tel>
                                </text>
                            </note>
                        </notepad>";

        Notepad notepad = parsexml(xml);

        foreach (var note in notepad.Notes)
        {
            Console.WriteLine($"ID: {note.id}");
            Console.WriteLine($"Date: {note.date}");
            Console.WriteLine($"Time: {note.time}");
            Console.WriteLine($"Subject: {note.subject}");
            Console.WriteLine($"Text: {note.text}");
            Console.WriteLine($"Telephone: {note.telephone}");
            Console.WriteLine();
        }
    }

    static Notepad parsexml(string xml)
    {
        XElement root = XElement.Parse(xml);
        Notepad notepad = new Notepad();

        foreach (XElement noteElement in root.Elements("note"))
        {
            Note note = new Note
            {
                id = int.Parse(noteElement.Attribute("id").Value),
                date = noteElement.Attribute("date").Value,
                time = noteElement.Attribute("time").Value,
                subject = noteElement.Element("subject").Value,
                text = noteElement.Element("text").Value
            };

            XElement telElement = noteElement.Element("text").Element("tel");
            if (telElement != null)
            {
                note.telephone = telElement.Value;
            }

            notepad.Notes.Add(note);
        }

        return notepad;
    }
}
