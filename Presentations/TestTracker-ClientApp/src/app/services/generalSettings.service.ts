import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment.prod";
import { HttpService } from "../shared/http-service.service";
import { Settings } from "../models/module-settings.model";

@Injectable({
  providedIn: 'root',
})
export class GeneralSettingsService extends HttpService<Settings> {


    constructor(protected override httpClient : HttpClient) {
      super(httpClient,environment.baseUrl,environment.endpoint);

  }

    getSettings(type:string) : Observable<any>{
    return this.httpClient.get<any>(
      `${environment.baseUrl}/${environment.endpoint}/${type}`
    );
  }
  addSettings(data: any,type:string){
    return this.httpClient.post<any>(
      `${environment.baseUrl}/${environment.endpoint}/${type}`,data
    );
  }
}
