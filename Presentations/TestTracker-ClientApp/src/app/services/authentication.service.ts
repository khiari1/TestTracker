import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.prod';
import { AuthenticationBaseService } from '../shared/authentication-base.service';
import { StorageService } from '../shared/storage.service';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService extends AuthenticationBaseService {
  /**
   *
   */
  constructor(
    override _httpClient: HttpClient,
    override storageService: StorageService
  ) {
    super(
      _httpClient,
      storageService,
      `${environment.baseUrl}/${environment.userLoginEndpoint}`
    );
  }
}
