import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { HttpService } from "../shared/http-service.service";
import { environment } from "src/environments/environment.prod";
import { Comment } from "../models/comment.model";
@Injectable ({
  providedIn:'root'
})

export class CommentService extends HttpService<Comment>{

  constructor(protected override httpClient : HttpClient) {
      super(httpClient,environment.baseUrl,environment.Comment);
  }
}
