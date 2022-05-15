using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BookCurlPro;

public class VNSaveFile : MonoBehaviour
{
    // Start is called before the first frame update
    public static VNSaveFile Instance;


    private int num_People_Pages = 0;
    private int num_Place_Pages = 0;
    private int num_Org_Pages = 0;
    private int num_Inj_Treat_Pages = 0;

    private int num_People_Paper = 0;
    private int num_Place_Paper = 0;
    private int num_Org_Paper = 0;
    private int num_Inj_Treat_Paper = 0;


    enum ChapterType {People, Places, Organizations, InjuriesAndTreatment};
    //page class: stores page info
    private class page{
        public Sprite pageImage;
        public bool isLoaded = false;
        public ChapterType type;
    }

    [SerializeField] private page[] pages;

    //private PlayerProgress loadedData;


    private class PlayerProgress
    {
        public List<Sprite> People_Pages = new List<Sprite>();
        public List<Sprite> Organization_Pages = new List<Sprite>();
        public List<Sprite> Place_Pages = new List<Sprite>();
        public List<Sprite> Injuries_and_Treatment_Pages = new List<Sprite>();
    }

    // public saveVNProgress(){
    //     PlayerProgress saveData = new PlayerProgress();

    //     saveData.People_Pages = People_Pages;

    //     saveData.Organization_Pages = Organization_Pages;

    //     saveData.Place_Pages = Place_Pages;

    //     saveData.Injuries_and_Treatment_Pages = Injuries_and_Treatment_Pages;

    //     DataSaver.saveData(saveData, "VNSaveData");
    // }

    // public loadVNProgess(){
    //     loadedData = DataSaver.loadData<PlayerProgress>("players");
    // }


    static List<Sprite> People_Pages = new List<Sprite>();
    static List<Sprite> Organization_Pages = new List<Sprite>();
    static List<Sprite> Place_Pages = new List<Sprite>();
    static List<Sprite> Injuries_and_Treatment_Pages = new List<Sprite>();


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void unlockPage(int n){
        if(n >= pages.Length || n < 0)
        {
            Debug.Log("Page out of range");
            return;
        }
        
        if(pages[n].isLoaded)
        {
            Debug.Log("Page already loaded");
            return;
        }
        

        //TODO: edit this!
        page temp = pages[n];

        if (temp.type == ChapterType.InjuriesAndTreatment)
        {
            Injuries_and_Treatment_Pages.Add(temp.pageImage);
            return;
        }
        else if (temp.type == ChapterType.People)
        {
            People_Pages.Add(temp.pageImage);
            return;
        }
        else if(temp.type == ChapterType.Places)
        {
            Place_Pages.Add(temp.pageImage);
        }
        else if (temp.type == ChapterType.Organizations)
        {
            Organization_Pages.Add(temp.pageImage);
            return;
        }


        Debug.Log("Not a noted category");
        return;
    }

    
    // Update is called once per frame


    
    public GameObject FrontPagePrefab;
    public GameObject BackPagePrefab;

    [SerializeField] private BookPro book;

    VNSaveFile JournalEntries;

    private int pageCount = 0;

    private Image tempImage;

    
    private void AddPaper(ChapterType section)
    {
        GameObject frontPage = Instantiate(FrontPagePrefab);
        GameObject backPage = Instantiate(BackPagePrefab);
        frontPage.transform.SetParent(book.transform, false);
        backPage.transform.SetParent(book.transform, false);
        Paper newPaper = new Paper();
        newPaper.Front = frontPage;
        newPaper.Back = backPage;
        Paper[] papers = new Paper[book.papers.Length + 1];


        int n = 1;
        if(section == ChapterType.People)
        {
            n += num_People_Paper;
        }
        for (int i = 0; i < n; i++)
        {
            papers[i] = book.papers[i];
        }



        for (int i = n + 1; i < book.papers.Length; i++)
        {
            papers[i] = book.papers[i];
        }
        papers[papers.Length - 1] = newPaper;
        book.papers = papers;
        //update the flipping range to contain the new added paper
        book.EndFlippingPaper = book.papers.Length - 1;
        book.UpdatePages();
    }
    
    


}



/*
    public Sprite[] getPeoplePages()
    {
        Sprite[] temp = new Sprite[People_Pages.Count];
        int c = 0;
        foreach(Sprite entry in People_Pages)
        {
            temp[c] = entry;
            c++;
        }
        return temp;
    }

    public Sprite[] getPlacesPages()
    {
        Sprite[] temp = new Sprite[Place_Pages.Count];
        int c = 0;
        foreach(Sprite entry in Place_Pages)
        {
            temp[c] = entry;
            c++;
        }
        return temp;
    }

    public Sprite[] getOrgPages()
    {
        Sprite[] temp = new Sprite[Organization_Pages.Count];
        int c = 0;
        foreach(Sprite entry in Organization_Pages)
        {
            temp[c] = entry;
            c++;
        }
        return temp;
    }

   public Sprite[] getInjAndTreat()
    {
        Sprite[] temp = new Sprite[Injuries_and_Treatment_Pages.Count];
        int c = 0;
        foreach(Sprite entry in Injuries_and_Treatment_Pages)
        {
            temp[c] = entry;
            c++;
        }
        return temp;
    }
    */


    // public void addPages()
    // {
    //     for (int i = 0; i < People_Pages.size(); i++)
    //     {
    //         if(i%2 == 0)
    //         {
    //             AddPaper(book);
    //             //book.papers[pageCount+3].Front            ???
    //             tempImage = book.papers[pageCount+3].Front.GetComponent<Image>();
    //         }
    //         else{
    //             //book.papers[pageCount+3].Back            ???
    //             tempImage = book.papers[pageCount+3].Back.GetComponent<Image>();
    //         }
            
    //     }
    // }