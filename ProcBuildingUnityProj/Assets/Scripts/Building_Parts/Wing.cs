using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing
{
    RectInt bounds;
	Story[] stories;
	Roof roof;
	
	public RectInt Bounds{get {return bounds;}}
	public Story[] Stories{get {return stories;}}
	public Roof GetRoof{get {return roof;}}
	
	public Wing(RectInt bounds){
		this.bounds=bounds;
	}
	public Wing(RectInt bounds, Story[] stories, Roof roof){
		this.bounds=bounds;
		this.stories=stories;
		this.roof=roof;
	}
	
	public override string ToString() {
		string wingStr = "Wing("+bounds.ToString()+")\n";
		foreach(Story s in stories){
			wingStr += "\t"+s.ToString()+"\n";
		}
		wingStr+= "\t"+roof.ToString()+"\n";
		return wingStr;
	}
}
