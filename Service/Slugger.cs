namespace pspbe.Service 
{
    public class Slugger
    {
        public string Sluggify (string title)
        {
            string noSpaces = title.Replace(" ", "-");
            return noSpaces.ToLower();
            //return "This is a slug";   
        }

    }
}