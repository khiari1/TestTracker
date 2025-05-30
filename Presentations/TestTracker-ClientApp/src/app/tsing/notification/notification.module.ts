import { NgModule } from "@angular/core";
import { NotificationItem, NotificationPanelComponent } from "./notification-panel.component";
import { CommonModule } from "@angular/common";
import { OverlayPanelModule } from "primeng/overlaypanel";
import { BadgeModule } from 'primeng/badge';

@NgModule({
    declarations: [NotificationPanelComponent,NotificationItem],
    imports: [
      CommonModule,
      OverlayPanelModule,
      BadgeModule
    ],
    providers:[],
    exports: [NotificationPanelComponent],
  })
  export class NotificationModule {}