namespace TasklySender.Request;

public record SendMailRequest(string TypeOfHTML, string To,  Dictionary<string, string> Props);
