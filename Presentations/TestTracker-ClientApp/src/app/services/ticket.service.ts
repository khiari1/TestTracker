import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment.prod";
import { Ticket, TicketState, TicketType } from "../models/ticket.model";
import { HttpService } from "../shared/http-service.service";

@Injectable({
  providedIn: 'root',
})
export class TicketService extends HttpService<Ticket>{
  /**
   *
   */
  constructor(protected override httpClient: HttpClient) {
    super(httpClient, environment.baseUrl, environment.ticketEndpoint);

  }

  addType(type: TicketType): Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/${this.endpoints}/AddType`, type)
  }

  getTypes(): Observable<TicketType[]> {
    return this.httpClient.get<TicketType[]>(`${this.baseUrl}/${this.endpoints}/GetTypes`)
  }

  addState(state: TicketState): Observable<any> {
    return this.httpClient.post(`${this.baseUrl}/${this.endpoints}/AddState`, state)
  }
  getStates(): Observable<TicketState[]> {
    return this.httpClient.get<TicketState[]>(`${this.baseUrl}/${this.endpoints}/GetStates`)
  }
}
