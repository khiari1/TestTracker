import {
  ChangeDetectionStrategy,
  Component,
  OnInit,
  ViewEncapsulation,
} from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { MenuItem } from 'primeng/api';
import { SideBarToggleService } from 'src/app/tsing/services/side-bar-toggle.service';

@Component({
  selector: 'app-side-bar-menu',
  template: `
    <div class="sidebar">
      <div style="width: 235px;">
        <ul class="menu-header mt-3">
          <ng-template ngFor let-item let-index="index" [ngForOf]="items">
            <li class="menu-item">
              <a
                [routerLink]="item.routerLink"
                (click)="onHeaderClick(item)"
                [ngClass]="{ 'expanded-item': item.expanded }"
                routerLinkActive="active"
              >
                <div class="flex align-items-center">
                  <div *ngIf="item.items">
                    <span
                      ><i
                        class="pi"
                        [ngClass]="{
                          'pi-chevron-down': item.expanded,
                          'pi-chevron-right': !item.expanded
                        }"
                      ></i
                    ></span>
                  </div>

                  <span class="icon pr-3" [pTooltip]="item.tooltip ?? ''"
                    ><img width="16px" height="16px" alt="" [src]="item.url"
                  /></span>
                  <span class="">{{ item.label }} </span>
                </div>
              </a>
              <ng-template
                ngFor
                let-subItem
                let-index="index"
                [ngForOf]="item.items"
              >
                <div
                  class="sub-menu-content border-buttom-1 border-400"
                  [ngClass]="{ hidden: !selectedItem(item) }"
                >
                  <ul class="sub-menu-item">
                    <li>
                      <a
                        [routerLink]="subItem.routerLink"
                        [queryParams]="subItem.queryParams"
                        routerLinkActive="active"
                      >
                        <div class="flex align-items-center">
                          <span class=" pr-2" [pTooltip]="subItem.tooltip ?? ''"
                            ><i [class]="subItem.icon"></i
                          ></span>
                          <span>{{ subItem.label }} </span>
                        </div>
                      </a>
                    </li>
                  </ul>
                </div>
              </ng-template>
            </li>
          </ng-template>
        </ul>
        <div>
        </div>
      </div>
    </div>
  `,
  changeDetection: ChangeDetectionStrategy.OnPush,
  encapsulation: ViewEncapsulation.None,
})
export class SideBarMenuComponent implements OnInit {
  constructor(
    protected sidebar: SideBarToggleService,
    public translate: TranslateService
  ) {}

  items: MenuItem[] = [];

  selectedItems: MenuItem[] = [];

  expanded: boolean = false;

  ngOnInit() {
    this.sidebar.expanded?.subscribe((ex) => {
      this.expanded = ex;
    });

    this.items = [
      {
        label: 'Dashboard',
        routerLink: 'widget',
        url: 'assets/png/statistic.png',
      },
      {
        label: 'Job',
        url: 'assets/png/icons8-pro-display-xdr-48.png',
        items: [
          {
            label: 'Recurring Jobs',
            icon: 'pi pi-cog',
            routerLink: 'job/reccuring',
          },
          {
            label: this.translate.instant('Enqueued'),
            icon: 'pi pi-play',
            routerLink: 'job/enqueued',
          },
          {
            label: this.translate.instant('Scheduled'),
            icon: 'pi pi-calendar-times',
            routerLink: 'job/schedule',
          },
          {
            label: this.translate.instant('Processing'),
            icon: 'pi pi-angle-double-right',
            routerLink: 'job/processing',
          },
          {
            label: this.translate.instant('Succeeded'),
            icon: 'pi pi-chevron-circle-right',
            routerLink: 'job/succeeded',
          },
          {
            label: this.translate.instant('Failed'),
            icon: 'pi pi-ban  ',
            routerLink: 'job/failed',
          },
        ],
      },
      {
        label: 'Monitoring',
        url: 'assets/png/icons8-report-file-48.png',
        items: [
          {
            label: 'All',
            routerLink: 'monitoring',
            icon: 'pi pi-desktop',
          },
          {
            label: 'Functionnal test',
            routerLink: 'monitoring',
            icon: 'pi pi-desktop',
            queryParams: { monitoringTestType: '0' },
          },
          {
            label: 'Unite test',
            routerLink: 'monitoring',
            icon: 'pi pi-sliders-h',
            queryParams: { monitoringTestType: '1' },
          },
        ],
      },
      {
        label: 'Monitoring detail',
        routerLinkActiveOptions: 'bg-blue-50',
        url: 'assets/png/icons8-test-tube-48.png',
        items: [
          {
            label: 'All',
            tooltip: 'All',
            routerLink: 'monitoring/detail',
            icon: 'pi pi-check-circle',
          },
          {
            label: 'Success',
            tooltip: 'Success',
            routerLink: 'monitoring/detail',
            queryParams: { State: '0' },
            icon: 'pi pi-check-circle',
          },
          {
            label: 'Warning',
            tooltip: 'Warning',
            routerLink: 'monitoring/detail',
            queryParams: { State: '1' },
            icon: 'pi pi-exclamation-circle',
          },
          {
            label: 'Failed',
            tooltip: 'Failed',
            routerLink: 'monitoring/detail',
            icon: 'pi pi-minus-circle',
            queryParams: { State: '2' },
          },
          {
            label: 'Error',
            tooltip: 'Error',
            routerLink: 'monitoring/detail',
            icon: 'pi pi-times-circle',
            queryParams: { State: '3' },
          },
          {
            label: 'Skipped',
            tooltip: 'Skipped',
            routerLink: 'monitoring/detail',
            icon: 'pi pi-circle',
            queryParams: { State: '4' },
          },
        ],
      },
      {
        label: 'Settings',
        routerLink: 'settings',
        routerLinkActiveOptions: 'bg-blue-50',
        url: 'assets/png/icons8-gear-32.png',
      }
    ];
  }

  selectedItem(item: MenuItem): boolean {
    return item.expanded ?? false;
  }

  onHeaderClick(item: MenuItem) {
    // this.collapseItems();
    item.expanded = !item.expanded;
  }

  collapseItems() {
    this.items.forEach((item) => (item.expanded = false));
  }
}
