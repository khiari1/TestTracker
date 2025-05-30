import { Injectable } from "@angular/core";

@Injectable({
  providedIn : 'root'
})
export class AvatarGenerator{

  private generateCanvas(color:string): HTMLCanvasElement {
    const canvas = document.createElement("canvas");
    const context = canvas.getContext("2d");

    canvas.width = 24;
    canvas.height = 24;

    // Draw background
    if(context){
      context.fillStyle = color;
      context.fillRect(0, 0, canvas.width, canvas.height);

      // Draw text
      context.font = "-apple-system, BlinkMacSystemFont, Segoe UI, Roboto, Helvetica, Arial, sans-serif, Apple Color Emoji, Segoe UI Emoji, Segoe UI Symbol";
      context.fillStyle = 'white';
      context.textAlign = "center";
      context.textBaseline = "middle";
      context.fillText('MR', canvas.width / 2, canvas.height - 11);
    }
    return canvas;
  }

  generate(color: string):string{
    const canvas = this.generateCanvas(color);
    return canvas.toDataURL("image/png",'1.0');
  }

  generateBlob(color: string):Blob | null {

    const canvas = this.generateCanvas(color);
    let blb : Blob | null = null;
     canvas.toBlob((blob)=>{
      blb = blob;
    });

    return blb;

  }
}
