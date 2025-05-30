import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';


@Injectable({
  providedIn: 'root',
})
export class FileService {
  constructor(private httpClient: HttpClient) {}

  upload(file: any, keyGroup: string, objectId: number): Observable<any> {
    // Create form data
    const formData = new FormData();

    // Store form name as "file" with file data
    formData.append('file', file, file.name);

    // Make http post request over api
    // with formData as req
    return this.httpClient.post(
      `${environment.baseUrl}/Files?folder=${keyGroup}&objectId=${objectId}`,
      formData
    );
  }
  getFiles(keyGroup: string, objectId: number): Observable<any> {
    return this.httpClient.get(
      `${environment.baseUrl}/Files?folder=${keyGroup}&objectId=${objectId}`
    );
  }

  delete( keyGroup: string, objectId: number,fileName: string): Observable<any> {        
    return this.httpClient.delete(
      `${environment.baseUrl}/Files?folder=${keyGroup}&objectId=${objectId}&fileName=${fileName}`
    );
  }

  download(fileName: string, keyGroup: string, objectId: number) {
    return this.httpClient.get(
      `${environment.baseUrl}/Files/Download?folder=${keyGroup}&objectId=${objectId}&fileName=${fileName}`,
      { responseType: 'blob' }
    );
  }

}
