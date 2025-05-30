import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  hubUrl: string;

  connection: any;

  notification: Subject<NotificationModel> = new Subject<NotificationModel>();

  constructor() {
    this.hubUrl = `${environment.baseUrl}/PushNotification`;
  }

  public async inisialize(): Promise<void> {
    try {
      this.connection = new signalR.HubConnectionBuilder()
        .withUrl(this.hubUrl)
        .withAutomaticReconnect()
        .build();

      this.setSignalrClientMethods();
      await this.connection.start();
      console.log(`SignalR connection success! connectionId: ${this.connection.connectionId}`);
    }
    catch (error) {
      console.log(`SignalR connection error: ${error}`);
    }
  }
  private setSignalrClientMethods(): void {
    this.connection.on('PushAsync', (message: NotificationModel) => {
      this.notification.next(message);
  });}

}

export interface NotificationModel{
  id : number;
  userId : string;
  severity: 'Success' | 'Warning' | 'Error' | 'Information';
  area: string;
  objectId: string;
  subject : string;
  message: string;
  value: any;

}
