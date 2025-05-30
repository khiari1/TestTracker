import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpService } from '../shared/http-service.service';
import { environment } from 'src/environments/environment.prod';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class ProjectFileService  {
  constructor(protected httpClient: HttpClient) {
    
  }

  get() : Observable<any>{
    return this.httpClient.get(
      `${environment.baseUrl}/${environment.projectFile}`
    );
}

  Upload(file: File,project : string): Observable<any> {
    const formData = new FormData();
    
    formData.append('file', file,file.name);
    return this.httpClient.post(
      `${environment.baseUrl}/${environment.projectFile}`,
      formData, { params: {projectName : project} }
    );
  }
}
