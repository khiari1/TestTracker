import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from '../../environments/environment.prod';
import { HttpService } from "../shared/http-service.service";
import { Feature } from "../models/feature.model";

@Injectable({
  providedIn: 'root'
})
export class FeatureService extends HttpService<Feature> {

  constructor(protected override httpClient: HttpClient) {
    super(httpClient, environment.baseUrl, environment.feature);
  }
}
