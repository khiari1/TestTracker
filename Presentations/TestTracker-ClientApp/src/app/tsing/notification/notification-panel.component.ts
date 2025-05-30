import { Component, Input, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { MenuItem } from 'primeng/api';
import {
  NotificationModel,
  NotificationService,
} from 'src/app/services/notification.service';

@Component({
  selector: 'tsi-notificationPanel',
  template: `
    <div class="menu-bar-sm">
      <ul class="">
        <li>
          <a
            class="flex align-items-center menu-item-sm"
            (click)="expanded = !expanded"
          >
              <i class="pi pi-bell"></i>
          </a>
          <div
            class="sub-menu-content"
            [ngClass]="expanded ? 'expanded' : ''"
          >
            <ul class="sub-menu-item">
              <li *ngIf="notifications.length == 0">
                <a>
                  <div>
                    <span class="text-center">No notifications</span>
                  </div>
                </a>
              </li>
              <li *ngFor="let notification of notifications">
                <a>
                  <div class="flex align-items-center">
                    <span>{{notification!.message}} </span>
                  </div>
                </a>
              </li>
            </ul>
          </div>
        </li>
      </ul>
    </div>
  
  <!-- <div class="pr-3 pl-3" (click)="op.toggle($event)">
                <i class="pi pi-bell text-2xl" pBadge severity="danger" [value]="notifications.length.toString()"></i>
             </div>
             <p-overlayPanel #op>
                <ng-template pTemplate>
                  <div style="width: 300px;">
                      <div *ngFor="let notificatio of notifications" class="border-bottom-1 border-200">
                        <tsi-notificationItem [notification]="notificatio"></tsi-notificationItem>
                      </div>
                    </div>
                </ng-template>
             </p-overlayPanel> -->
  `,
})
export class NotificationPanelComponent implements OnInit {
  notifications: NotificationModel[] = [];
  constructor(public notificationService: NotificationService) {}

  ngOnInit(): void {
    this.notificationService.notification.subscribe((notifications) => {
      if(notifications.area == "Notification"){
        this.notifications.push(notifications);
      }      
    });
  }
    menuItems: MenuItem[] = [];
    expanded: boolean = false;
  
    performClick(menuItem: MenuItem) {
      this.performCommand(menuItem);
      this.toogle(menuItem);
    }
  
    toogle(menuItem: MenuItem) {
      let expanded = menuItem.expanded;
      this.collapseItems();
      if (menuItem.items) {
        menuItem.expanded = expanded ? false : true;
      }
    }
  
    performCommand(menuItem: MenuItem) {
      if (menuItem.command) {
        menuItem.command();
      }
    }
  
    collapseItems(){
      let menuItems = this.menuItems.filter(m =>m.items) 
      menuItems.forEach(item => {
        item.expanded = false;
      })
    }
}

@Component({
  selector: 'tsi-notificationItem',
  template: `<div class="p-2 flex">
                <img
                  class="mr-3"
                  [style]="'width: 23px; height: 23px;'"
                  [src]="'assets/svg/' + severity"
                  [title]="notification!.subject"
                />
                <span>{{notification!.message}}</span>
            </div>`,
})
export class NotificationItem implements OnInit {

  @Input() notification: NotificationModel | undefined;

  get severity(): string {
    switch (this.notification!.severity) {
      case 'Success':
        return 'success.svg';
      case 'Warning':
        return 'warning.png';
      case 'Error':
        return 'error.svg';
      case 'Information':
        return 'success.svg';
      default:
        return 'no-entry.png';
    }
  }
  ngOnInit(): void {}
}


