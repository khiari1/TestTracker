import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserPassModel } from '../models/user.model';
import { Filter } from '../tsing/views/filter.model';
import { Inject, Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { BASEURL_CONFIG } from '../config';

enum type { value1 ,value2 ,value3};
/**
 * Performs HTTP requests.
*/
export class HttpService<T> {
  /**
   * Create a point.
   * @param {HttpClient} httpClient - The httpClient for performing http request.
   * @param {string} baseUrl - The base URL .
   * @param {string} endpoints - The endpoint URL.
   */

  url: string | undefined;
  constructor(
    protected httpClient: HttpClient,
    protected baseUrl: string,
    protected endpoints: string
  ) {}
  setEndpoints(endPs: string) {
    this.endpoints = endPs;
  }
  getAll(): Observable<any>;
  getAll(filter: Filter): Observable<any>;
  getAll(filter?: Filter): Observable<any> {
    if(filter == undefined){
      return this.httpClient.get<any>(`${this.baseUrl}/${this.endpoints}`);
    }else{
      return this.httpClient.post<any>(`${this.baseUrl}/${this.endpoints}/Query`,filter);
    }
    
  }

  resetUserPassword(model: UserPassModel) {
    return this.httpClient.put<UserPassModel>(
      `${this.baseUrl}/${this.endpoints}/ResetPassword`,
      model
    );
  }

  delete(id: string): Observable<any> {
    if (id === null) throw new Error("The parameter 'id' cannot be null.");

    return this.httpClient.delete(`${this.baseUrl}/${this.endpoints}/${id}`);
  }

  deleteItems(ids: string[]): Observable<any> {
    if (ids === null) throw new Error("The parameter 'id' cannot be null.");

    return this.httpClient.delete(`${this.baseUrl}/${this.endpoints}`, {
      body : ids
    });
  }

  create(model: T): Observable<any> {
    return this.httpClient.post<T>(`${this.baseUrl}/${this.endpoints}`, model);
  }

  update(model: T): Observable<T>;
  update(model: T, id: string): Observable<T>;

  update(model: T, id?: string): Observable<T> {
    if (!id) {
      return this.httpClient.put<T>(`${this.baseUrl}/${this.endpoints}`, model);
    } else {
      return this.httpClient.put<T>(
        `${this.baseUrl}/${this.endpoints}/${id}`,
        model
      );
    }
  }
  Update(id: number, model: T) {
    return this.httpClient.put<T>(
      `${this.baseUrl}/${this.endpoints}/${id}`,
      model
    );
  }

  getById(id: string): Observable<any> {
    if (id === null || id === undefined)
      throw new Error("The parameter 'id' cannot be null.");
    return this.httpClient.get<any>(`${this.baseUrl}/${this.endpoints}/${id}`);
  }

  getAllPaged(model: any): Observable<any> {
    return this.httpClient.post<any>(
      `${this.baseUrl}/${this.endpoints}`,
      model
    );
  }
  refresh(model: T): Observable<any> {
    return this.httpClient.post<any>(
      `${this.baseUrl}/${this.endpoints}`,
      model
    );
  }


}

/**
 * Performs HTTP requests.
*/
export class HttpClientBaseService<T> {
  /**
   * Create a point.
   * @param {HttpClient} httpClient - The httpClient for performing http request.
   * @param {string} baseUrl - The base URL .
   * @param {string} endpoints - The endpoint URL.
   */

  url: string | undefined;
  constructor(
    protected httpClient: HttpClient,
    protected baseUrl: string,
    protected endpoints: string
  ) { }
  
  setEndpoints(endPs: string) {
    this.endpoints = endPs;
  }
  get(): Observable<any>;
  get(filter: Filter): Observable<any>;
  get(filter?: Filter): Observable<any> {
    if(filter == undefined){
      return this.httpClient.get<any>(`${this.baseUrl}/${this.endpoints}`);
    }else{
      return this.httpClient.post<any>(`${this.baseUrl}/${this.endpoints}/Query`,filter);
    }
    
  }

  delete(id: string): Observable<any> {
    if (id === null) throw new Error("The parameter 'id' cannot be null.");

    return this.httpClient.delete(`${this.baseUrl}/${this.endpoints}/${id}`);
  }

  batchDelete(ids: any[]): Observable<any> {
    return this.httpClient.delete(`${this.baseUrl}/${this.endpoints}`,{
      body : ids
    });
  }

  create(model: any): Observable<any> {
    return this.httpClient.post<any>(`${this.baseUrl}/${this.endpoints}`, model);
  }

  update(id: string, model : any): Observable<any> {

      return this.httpClient.put<T>(
        `${this.baseUrl}/${this.endpoints}/${id}`,
        model
      );
  }

  getById(id: string): Observable<any> {
    if (id === null || id === undefined)
      throw new Error("The parameter 'id' cannot be null.");
    return this.httpClient.get<any>(`${this.baseUrl}/${this.endpoints}/${id}`);
  }

  refresh(model: T): Observable<any> {
    return this.httpClient.post<any>(
      `${this.baseUrl}/${this.endpoints}`,
      model
    );
  }

  calculate(model: T): Observable<any> {
    return this.httpClient.post<any>(
      `${this.baseUrl}/${this.endpoints}/Calculate`,
      model
    );
  }

}

@Injectable({
  providedIn : null
})
export class HttpClientService extends HttpClientBaseService<any> {
  constructor(protected override httpClient: HttpClient, @Inject(BASEURL_CONFIG) protected override baseUrl: string) {
    super(httpClient, baseUrl, '');
  }
}
