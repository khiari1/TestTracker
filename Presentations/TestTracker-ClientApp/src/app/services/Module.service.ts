import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Module } from "../models/module.model";
import { environment } from '../../environments/environment.prod';
import { HttpService } from "../shared/http-service.service";
import { MonitoringMonitoringDetails } from "../models/monitoring-detail.model";
import { ModuleSummary } from "../models/monitoring.model";
import { Settings } from "../models/module-settings.model";

@Injectable({
    providedIn: 'root'
  })
  export class ModuleService extends HttpService<Module> {

    constructor(protected override httpClient : HttpClient) {
      super(httpClient,environment.baseUrl,environment.module);

  }
  getModuleDetails(
    groupBy: 'non',
    searchPattern?: string,
    searchId?: number,
    moduleIds?: number[]
  ): Observable<Settings[]>;

  getModuleDetails(
    groupBy: 'module',
    searchPattern?: string,
    searchId?: number,
    moduleIds?: number[]
  ): Observable<ModuleSummary[]>;

  getModuleDetails(
    groupBy: any,
    searchPattern?: string,
    searchId?: number,
    moduleIds?: number[]
  ): Observable<ModuleSummary[]> | Observable<Settings[]> {
    return this.httpClient.post<any[]>(
      `${environment.baseUrl}/${environment.moduleEndpoint}/ModuleDetails/${groupBy}`,
      {
        searchPattern : searchPattern,
        searchId : searchId,
        moduleIds : moduleIds
      }
    );
    }
  }
