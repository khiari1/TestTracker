import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';
import { Observable, map } from 'rxjs';
import { ModuleSummary, Monitoring } from '../models/monitoring.model';
import {
  MonitoringDetailsChart,
  MonitoringMonitoringDetails,
} from '../models/monitoring-detail.model';
import { IJobModel } from '../dashboard/monitoring/create-job.component';
import { Filter, LogicalOperator, Operator } from '../tsing/views/filter.model';

@Injectable({
  providedIn: 'root',
})
export class MonitoringService {
  constructor(private http: HttpClient) {}

  getById(id : number): Observable<Monitoring>{
      return this.http.get<Monitoring>(`${environment.baseUrl}/${environment.monitoringEndpoint}/${id}`)
  }


  getMonitoringDetails(filter : any): Observable<MonitoringMonitoringDetails[]> {
    return this.http.post<any[]>(
      `${environment.baseUrl}/${environment.monitoringEndpoint}/Details/Query`,filter
    );
  }
  getDetailsById(id: any): Observable<any[]> {
    let filter: Filter = {
      keyword: '',
      operator: LogicalOperator.And,
      propertyfilters : [{propertyName : 'Id',operator : Operator.Equal,propertyType : '',propertyText: '',propertyValue:id}]
    }
    return this.http.post<any[]>(
      `${environment.baseUrl}/${environment.monitoringEndpoint}/Details/Query`,filter
    );
  }
  getAllMonitoring(filter : any) {
    return this.http.post<Monitoring[]>(
      `${environment.baseUrl}/${environment.monitoringEndpoint}/query`,
      filter
    );
  }

  addMonitoring(monitoring: Monitoring) {
    return this.http.post<Monitoring>(
      `${environment.baseUrl}/${environment.monitoringEndpoint}`,
      monitoring
    );
  }

  getByIdMonitoring(id: number) {
    if (id === null) throw new Error("The parameter 'id' cannot be null.");
    return this.http.get<Monitoring[]>(
      `${environment.baseUrl}/${environment.monitoringEndpoint}/${id}`
    );
  }

  deleteMonitoring(id: number) {
    if (id === null) throw new Error("The parameter 'id' cannot be null.");
    return this.http.delete<Monitoring[]>(
      `${environment.baseUrl}/${environment.monitoringEndpoint}/${id}`
    );
  }

  updateMonitoring(id: number, monitoring: Monitoring) {
    return this.http.put<Monitoring>(
      `${environment.baseUrl}/${environment.monitoringEndpoint}/${id}`,
      monitoring
    );
  }

  runTest(id: number) {
    this.http
      .get<Monitoring>(
        `${environment.baseUrl}/${environment.monitoringEndpoint}/RunTest/${id}`
      )
      .subscribe();
  }

  runMonitorings(ids: number[]) {
    let queryParams = new HttpParams();
      queryParams = queryParams.append('ids', JSON.stringify(ids));

    this.http
      .post<Monitoring>(
        `${environment.baseUrl}/${environment.monitoringEndpoint}/RunTest`,
        ids
      )
      .subscribe();
  }

  recurringJob(recurringJobId : string,interval : number) {
    let queryParams = new HttpParams();
    if (interval) {
      queryParams = queryParams.append('minuteInterval', interval);
    }
    if (recurringJobId) {
      queryParams = queryParams.append('recurringJobId', recurringJobId);
    }
    this.http
      .get<Monitoring>(
        `${environment.baseUrl}/${environment.monitoringEndpoint}/RecurringJob`,
        { params: queryParams }
      )
      .subscribe();
  }
  createScheduleJob(job: IJobModel) {
    this.http
      .post<Monitoring>(
        `${environment.baseUrl}/${environment.monitoringEndpoint}/ScheduleJob`,job
      )
      .subscribe();
  }
  createRecurringJob(job: IJobModel) {
    this.http
      .post<Monitoring>(
        `${environment.baseUrl}/${environment.monitoringEndpoint}/RecurringJob`,job
      )
      .subscribe();
  }


  exportData(filter : any): any {
      return this.http.post(
        `${environment.baseUrl}/${environment.monitoringEndpoint}/export`,
        filter,
        {observe: 'response', responseType: 'blob' }
      );
    
  }
  importData(file: File): Observable<any> {
    const formData: FormData = new FormData();

    formData.append('file', file);

    return this.http.post(
      `${environment.baseUrl}/${environment.monitoringEndpoint}/import`,
      formData
    );
  }
}
export enum State {
  Success = 0,
  Warning = 1,
  Failed = 2,
  Error = 3,
  Skipped = 4,
}
