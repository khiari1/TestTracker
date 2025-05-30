import { Component, EventEmitter, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { SideBarToggleService } from 'src/app/tsing/services/side-bar-toggle.service';

@Component({
  selector: 'board-layout',
  template: `
  
    <div class="wrapper" [ngClass]="{ 'expanded': expanded }">
    
      <app-header-menu [user]="user"></app-header-menu>

      <div class="container surface-0">      
        <router-outlet></router-outlet>
      </div>
    </div>
  `,
})
export class DashboardLayoutComponent implements OnInit {
  user?: any | null;
  expanded: boolean = true;
  constructor(
    private readonly authenticationService: UserService,
    private readonly sidBar: SideBarToggleService
  ) {}

  ngOnInit(): void {
    if (this.authenticationService.me()) {
      this.authenticationService.me().subscribe((data) => {
        this.user = data;
      });
      this.sidBar.expanded?.subscribe((ex) => {
        this.expanded = ex;
      });
    }
  }
}
