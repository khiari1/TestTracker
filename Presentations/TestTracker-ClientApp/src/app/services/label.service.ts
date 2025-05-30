import { Label } from "../models/label.model";
import { HttpService } from "../shared/http-service.service";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class MenuService extends HttpService<Label> {

  constructor(protected override httpClient: HttpClient) {
    super(httpClient, environment.baseUrl, environment.label);

  }
}
