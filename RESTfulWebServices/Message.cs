namespace RESTfulWebServices
{
    public class Message
    {
        public int Id { get; set; }
        public string? Author { get; set; }
        public string? Content { get; set; }
        public DateTime Created { get; set; }

        public static List<Message> getSampleMessages()
        {
            var messages = new List<Message>();

            // Przykładowa wiadomość 1
            messages.Add(new Message
            {
                Id = 1,
                Author = "Peter",
                Content = "Let's conquer the world!",
                Created = DateTime.Now
            });

            // Przykładowa wiadomość 2
            messages.Add(new Message
            {
                Id = 2,
                Author = "John",
                Content = "I will avenge my dog.",
                Created = DateTime.Now.AddHours(1)
            });

            // Przykładowa wiadomość 3
            messages.Add(new Message
            {
                Id = 3,
                Author = "Andrew",
                Content = "I shall rule.",
                Created = DateTime.Now.AddDays(1)
            });

            // Przykładowa wiadomość 4
            messages.Add(new Message
            {
                Id = 4,
                Author = "Diana",
                Content = "Tell me the truth!",
                Created = new DateTime(2025, 5, 19, 12, 0, 0)
            });
            return messages;
        }
    }
}
