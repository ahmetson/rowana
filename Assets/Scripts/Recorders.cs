using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using System.IO;

public class Recorders : MonoBehaviour
{
    public AudioRenderer rec;

    /*
    private int bufferSize;
    private int numBuffers;
    private int outputRate = 44100;
    private string fileName = "recTest.wav";
    private int headerSize = 44;

    private bool recOutput;
 
    private FileStream fileStream;

    private AudioSource pausAudio;

    public void Awake()
    {

        AudioSettings.outputSampleRate = outputRate;
    }

    public void Start()
    {

        AudioSettings.GetDSPBufferSize(out int bufferSize, out int numBuffers); 

    }

    public void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            print("rec");
            if (recOutput == false)
            {
                StartWriting(fileName);
                recOutput = true;
            }
            else
            {
                recOutput = false;
                WriteHeader();
                print("rec stop");
            }
        }
    }

    public void StartWriting(String name)
    {
        fileStream = new FileStream(name, FileMode.Create);
        var emptyByte = new byte();

        for (var i  = 0; i < headerSize; i++) 
    {
            fileStream.WriteByte(emptyByte);
        }
    }

    public void OnAudioFilterRead(float[] data, int channels)
    {
        if (recOutput)
        {
            ConvertAndWrite(data); 
        }
    }
    public void ConvertAndWrite(float[] dataSource)
    {
        var intData : Int16[] = new Int16[dataSource.length];
       

        Byte bytesData[] = new Byte[dataSource.length * 2];
        

        int rescaleFactor = 32767;

        for (var i : int = 0; i < dataSource.length; i++)
    {
            intData[i] = dataSource[i] * rescaleFactor;
            var byteArr : Byte[] = new Byte[2];
            byteArr = BitConverter.GetBytes(intData[i]);
            byteArr.CopyTo(bytesData, i * 2);
        }

        fileStream.Write(bytesData, 0, bytesData.length);
    }
    */
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rec.Rendering = true;

        } else if (Input.GetKeyDown(KeyCode.T))
        {
            rec.Save("new1");
            rec.Rendering = false;
        }
    }
    
}