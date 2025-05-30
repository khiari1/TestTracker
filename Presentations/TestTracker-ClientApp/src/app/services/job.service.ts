import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Job } from '../models/job.model';
import { environment } from '../../environments/environment.prod';
import { HttpService } from '../shared/http-service.service';

@Injectable({
  providedIn: 'root',
})
export class JobService extends HttpService<Job> {
  constructor(protected override httpClient: HttpClient) {
    super(httpClient, environment.baseUrl, environment.job);
  }

  //htttp get methode
  recurringJobs(filter : any) : Observable<any[]>{
      return this.httpClient.post<any[]>(`${this.baseUrl}/${this.endpoints}/Reccuring`,filter);
  }
  ScheduledJobs(filter : any) :Observable<any[]>{
    return this.httpClient.post<any[]>(`${this.baseUrl}/${this.endpoints}/Scheduled`,filter);
}
  succeededJobs(filter : any) : Observable<any[]>{
    return this.httpClient.post<any[]>(`${this.baseUrl}/${this.endpoints}/Succeeded`,filter);
  }
  failedJobs(filter : any) : Observable<any[]>{
    return this.httpClient.post<any[]>(`${this.baseUrl}/${this.endpoints}/Failed`,filter);
  }
  enqueuedJobs(filter : any) : Observable<any[]>{
    return this.httpClient.post<any[]>(`${this.baseUrl}/${this.endpoints}/Enqueued`,filter);
  }
  processingJobs(filter : any):Observable<any[]>{
    return this.httpClient.post<any[]>(`${this.baseUrl}/${this.endpoints}/Processing`,filter);
  }

  //Http post methode
  requeuJobs(jobIds : string[]):Observable<any>{
    return this.httpClient.post(`${this.baseUrl}/${this.endpoints}/Requeue`,jobIds);
  }
  triggerReccuringJobs(jobIds : string[]):Observable<any>{
    return this.httpClient.post(`${this.baseUrl}/${this.endpoints}/Reccuring/Trigger`,jobIds);
  }

  //http delete methode
  deleteJobs(jobIds : string[]):Observable<any>{
        return this.httpClient.delete(`${this.baseUrl}/${this.endpoints}`,
    {body : jobIds});
  }

  deleteReccuringJobs(jobIds : string[]):Observable<any>{
    return this.httpClient.delete(`${this.baseUrl}/${this.endpoints}/Reccuring`,
    {body : jobIds});
  }
}
