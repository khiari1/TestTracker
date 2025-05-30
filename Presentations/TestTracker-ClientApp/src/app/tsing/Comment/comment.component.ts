import { Component, Input, OnInit, Output, ViewChild } from '@angular/core';
import { MessageService } from 'primeng/api';
import { DialogService } from 'primeng/dynamicdialog';
import { Inplace } from 'primeng/inplace';
import { CommentService } from 'src/app/services/comment.service';
import { Comment } from 'src/app/models/comment.model';
import { RequestInterceptorService } from 'src/app/services/token.interceptor.service';
import { UserService } from 'src/app/services/user.service';
import { UserModel } from 'src/app/models/user.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-comment',
  template: `
    <div class="row">
      <div class="col-12">
        <p-editor
          #editor
          [(ngModel)]="comment.content"
          [style]="{ height: '100px' }"
        ></p-editor>
        <span> </span>
        <!--  -->
        <div class="card">
          <div
            class="flex justify-content-end flex-wrap card-container purple-container"
          >
            <div
              class="flex align-items-center justify-content-center font-bold text-white border-round m-2"
            >
              <button
                [hidden]="hide"
                pButton
                
                class="mr-1"
                (click)="save()"
              >
                {{ text }}
              </button>

              <button
                pButton
                
                label="Cancel"
                (click)="Cancel(comment)"
              ></button>
            </div>
          </div>
        </div>
      </div>

      <div class="card">
        <p-inplace #inplace [hidden]="hide">
          <ng-template pTemplate="display"> Click to Edit </ng-template>
          <ng-template pTemplate="content">
            <div *ngFor="let item of comments" let i="index">
              <div class="row">
                <div>
                  <p-editor
                    #editor2
                    [(ngModel)]="item.content"
                    [style]="{ height: '100px' }"
                  >
                  </p-editor>
                </div>

                <div>
                  <div>
                    <div
                      class="flex align-items-left justify-content-right  bg-white-500 font-bold text-black border-round m-2"
                    >
                      <span
                        >Created by :{{ item.user.firstName }}
                        {{ item.user.lastName }}<br />
                        On :{{ item.date | date : 'd MMM y, h:mm' }}</span
                      >
                    </div>
                  </div>
                </div>

                <div
                  class="flex justify-content-end flex-wrap card-container purple-container"
                >
                  <div
                    class="flex align-items-center justify-content-center font-bold text-black border-round m-2"
                  >
                    <div
                      *ngIf="item.isCurrentUser"
                      [hidden]="!item?.isCurrentUser"
                    >
                      <button
                        pButton
                        
                        label="Edit"
                        styleClass="p-button-success"
                        class="mr-1"
                        (click)="Edit(item)"
                      ></button>
                      <button
                        pButton
                        
                        label="Delete"
                        (click)="Delete(item.id, item)"
                      ></button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </ng-template>
        </p-inplace>
      </div>
    </div>
  `,
  providers: [DialogService],
})
export class CommentComponent implements OnInit {
  @Input() keyGroup: string | undefined;

  @Input() relatedObject: number | undefined;

  @ViewChild('inplace') ip!: Inplace;

  comment: Comment = new Comment();
  users: UserModel[] = [];
  id: any;

  hide: boolean = true;

  date!: Date;

  visible: boolean = false;

  comments: any;

  comm: Array<string> = [];
  com: any;
  config: any;
  commentList: any;

  isLoading: boolean = false;
  isFalse: boolean = false;

  public text: string = 'Save';
  currentUser?: UserModel[];
  curUser: any;

  user: any;
  msg!: Date | undefined;

  formState: any = { state: 'Add', id: null };

  constructor(
    private commentService: CommentService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.text === 'Save';
  }

  save() {
    this.text = 'Add';
    this.comment.objectId = this.relatedObject;
    this.comment.keyGroup = this.keyGroup;
    if (this.keyGroup && this.relatedObject) {
      this.commentService.create(this.comment).subscribe({
        next: () => {},
        error: (data) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Info Message',
            detail: JSON.stringify(data.error),
          });
        },
      });

      this.comment.content = '';
      this.hide = false;
      this.ip.activate();
      this.loadData();
    }
  }

  Edit(item: Comment) {
    const now = new Date();

    item.date = now;

    this.id = item.id;

    if (this.keyGroup && this.relatedObject) {
      this.commentService.Update(this.id, item).subscribe({
        next: () => {
          this.loadData();
        },
        error: (data) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Info Message',
            detail: JSON.stringify(data.error),
          });
        },
      });
    }
  }
  Delete(id: any, item: Comment) {
    id = item.id;
    this.comment.objectId = this.relatedObject;
    this.comment.keyGroup = this.keyGroup;
    if (this.keyGroup && this.relatedObject) {
      var observer = this.commentService.delete(id).subscribe({
        next: () => {
          this.loadData();
        },
        error: (data) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Info Message',
            detail: JSON.stringify(data.error),
          });
        },
      });
    }
  }
  loadData() {
    if (this.keyGroup && this.relatedObject) {
      this.commentService.getAll().subscribe((data) => {
        this.comments = data;
      });
    }
  }
  GetCommentById(id: any) {
    if (this.keyGroup && this.relatedObject) {
      this.commentService.getById(id).subscribe((data) => {
        this.comment = data;
        this.user = this.comment;
      });
    }
  }

  Cancel(item: Comment) {
    this.id = item.content;
    this.comment.objectId = this.relatedObject;
    this.comment.keyGroup = this.keyGroup;
    if (this.keyGroup && this.relatedObject) {
      item.content = '';
    }
  }
}
