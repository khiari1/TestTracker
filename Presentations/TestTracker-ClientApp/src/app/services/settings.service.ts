import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';
import { Module } from '../models/module.model';
import { Observable } from 'rxjs';
import { Menu } from '../models/menu.model';
import { Settings } from '../models/module-settings.model';

@Injectable({
  providedIn: 'root',
})
export class SettingsService {
  constructor(private http: HttpClient) {}

  addModule(module: Module): Observable<any> {
    return this.http.post<any>(
      `${environment.baseUrl}/${environment.endpoint}/AddModule`,
      module
    );
  }

  getModulesettings(searchPattern?: string): Observable<Settings[]> {
    let url = `${environment.baseUrl}/${environment.endpoint}/GetAllModule`;
    if (searchPattern !== undefined) {
      url = url + `/${searchPattern}`;
    }
    return this.http.get<Settings[]>(url);
  }
  addMenu(menu: Menu): Observable<any> {
    return this.http.post<any>(
      `${environment.baseUrl}/${environment.endpoint}/AddMenu`,
      menu
    );
  }
}
