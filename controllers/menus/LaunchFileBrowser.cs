using UnityEngine;
using System.Collections;

public class LaunchFileBrowser : MonoBehaviour {
	//skins and textures
	public Texture2D file,folder,back,drive;

	//initialize file browser
	FileBrowser fb = new FileBrowser();
	string output = "";

	bool cancel, select;

	// Use this for initialization
	void Start () {
		fb.fileTexture = file; 
		fb.directoryTexture = folder;
		fb.backTexture = back;
		fb.driveTexture = drive;
		fb.searchRecursively = true;

		cancel = false;
		select = false;
	}

	void OnGUI(){
		if(fb.draw()){ //true is returned when a file has been selected
			//the output file is a member if the FileInfo class, if cancel was selected the value is null
			if(fb.outputFile != null){
				output = fb.outputFile.ToString();
			}
			Select = fb.select;
			Cancel = fb.cancel;
		}
	}

	public bool Select {
		get {
			return this.select;
		}
		set {
			select = value;
		}
	}
	
	public bool Cancel {
		get {
			return this.cancel;
		}
		set {
			cancel = value;
		}
	}
	
	public string Output {
		get {
			return this.output;
		}
		set {
			output = value;
		}
	}
}
