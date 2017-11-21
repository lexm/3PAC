using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;
using SimpleJSON;

public class MetaCoding : MonoBehaviour
{

    public Rigidbody pac;
    public int speed;

    // Use this for initialization
    void Start()
    {
        print("Started webservice import...\n");
        pac = GetComponent<Rigidbody>();
        StartCoroutine(DownloadWebService());
    }

    IEnumerator DownloadWebService()
    {
        while (true)
        {
            WWW w = new WWW("http://three-pac.herokuapp.com/?command");
            yield return w;

            print("Waiting for webservice\n");

            yield return new WaitForSeconds(1f);

            print("Received webservice\n");

            ExtractCommand(w.text);

            print("Extracted information");

            WWW y = new WWW("http://three-pac.herokuapp.com/?command=empty");
            yield return y;

            print("Cleaned webservice");

            yield return new WaitForSeconds(5);
        }
    }

    void ExtractCommand(string json)
    {
        var jsonstring = JSON.Parse(json);
        string command = jsonstring["command"];
        print(command);
        if (command == null) { return; }
        string[] commands_array = command.Split(" "[0]);
        if (commands_array.Length < 2)
        {
            return;
        }
        if (commands_array[0] == "go")
        {
            MovePac(commands_array[1]);
        }
    }

    void MovePac(string direction)
    {
        Vector3 dir = new Vector3();
        
        switch (direction)
        {
            case "left":
                dir = Vector3.left;
                // pac.AddForce(Vector3.left);
                break;
            case "right":
                dir = Vector3.right;
                // pac.AddForce(Vector3.right);
                break;
            case "front":
                dir = Vector3.forward;
                // pac.AddForce(Vector3.forward);
                break;
            case "back":
                dir = Vector3.back;
                // pac.AddForce(Vector3.back);
                break;
        }
        pac.AddForce(dir * speed);
    }


    // Update is called once per frame
    void Update()
    {

    }
}