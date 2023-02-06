using UnityEngine;
using System;
using System.IO;
using UnityEditor.VersionControl;

public class AudioRenderer : MonoBehaviour
{
    #region Fields, Properties, and Inner Classes
    
    private const int HEADER_SIZE = 44;
    private const short BITS_PER_SAMPLE = 16;
    private const int SAMPLE_RATE = 48000;

    private int channels = 2;

    public MemoryStream outputStream;
    public BinaryWriter outputWriter;
    
    public bool Rendering = false;

    public enum Status
    {
        UNKNOWN,
        SUCCESS,
        FAIL,
        ASYNC
    }

    
    public class Result
    {
        public Status State;
        public string Message;

        public Result(Status newState = Status.UNKNOWN, string newMessage = "")
        {
            this.State = newState;
            this.Message = newMessage;
        }
    }
    #endregion

    public AudioRenderer()
    {
        this.Clear();
    }

    
    public void Clear()
    {
        this.outputStream = new MemoryStream();
        this.outputWriter = new BinaryWriter(outputStream);
    }

    
    public void Write(float[] audioData)
    {
        

        
        for (int i = 0; i < audioData.Length; i++)
        {
            
            
            this.outputWriter.Write((short)(audioData[i] * (float)Int16.MaxValue));
        }
    }

   
    public void OnAudioFilterRead(float[] data, int channels)
    {
        if (this.Rendering)
        {
            
            this.channels = channels;

            
            this.Write(data);
        }

    }

    
    #region File I/O
    public AudioRenderer.Result Save(string filename)
    {
        Result result = new AudioRenderer.Result();
        
        if (outputStream.Length > 0)
        {
            
            this.AddHeader();

            
            if (filename.Length > 0)
            {
                
                if (File.Exists(filename))
                    Debug.LogWarning("Overwriting " + filename + "...");

                outputStream.Position = 0;
                
                FileStream fs = File.OpenWrite(filename);

                this.outputStream.WriteTo(fs);

                fs.Write(System.Text.Encoding.ASCII.GetBytes("WAVE"));

                fs.Close();

                
                Debug.Log("Finished saving to " + filename + ".");
            }

            result.State = Status.SUCCESS;
        }
        else
        {
            Debug.LogWarning("There is no audio data to save!");

            result.State = Status.FAIL;
            result.Message = "There is no audio data to save!";
        }

        return result;
    }

    private void AddHeader()
    {
        
        outputStream.Position = 0;

        
        long numberOfSamples = outputStream.Length / (BITS_PER_SAMPLE / 8);

        
        MemoryStream newOutputStream = new MemoryStream();
        BinaryWriter writer = new BinaryWriter(newOutputStream);

        writer.Write(0x46464952); 

       
        writer.Write((int)(HEADER_SIZE + (numberOfSamples * BITS_PER_SAMPLE * channels / 8)) - 8);

        writer.Write(0x45564157); 
        writer.Write(0x20746d66); 
        writer.Write(16);

        
        writer.Write((short)1);

        
        writer.Write((short)channels);

        
        writer.Write(SAMPLE_RATE);

        writer.Write(SAMPLE_RATE * channels * (BITS_PER_SAMPLE / 8));
        writer.Write((short)(channels * (BITS_PER_SAMPLE / 8)));

        
        writer.Write(BITS_PER_SAMPLE);

        
        writer.Write(0x61746164);

        
        writer.Write((int)(numberOfSamples * BITS_PER_SAMPLE * channels / 8));

        //writer.Write(System.Text.Encoding.ASCII.GetBytes("WAVE"));


        this.outputStream.WriteTo(newOutputStream);

        
        this.outputStream = newOutputStream;
    }
    #endregion
}