import { Component, OnInit, Sanitizer } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UserAzureAdModel } from 'src/app/models/user.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  template: `

<form-view
  [dataSource]="'v1/Users'"
  [(data)]="user"
  [breadcrumbLinks]="[
    {
      routerLink: '/dashboard',
      label: 'Dashboard'
    },
    {
      routerLink: '/dashboard/settings',
      label: 'Settings'
    },
    {
      routerLink: '/dashboard/settings/users',
      label: 'Users'
    },
    {
      routerLink: '/dashboard/settings/users',
      queryParams: { id: user?.id },
      label: user?.userName ?? ''
    }
  ]"
  [title]="user?.userName ?? ''"
>
  <ng-template #content>
  <div class="grid" *ngIf="user">
      <div class="col-12 md:col-6">

        <div class="p-fluid ">
          <div class="field grid">
            <label htmlfor="name3" class="col-12 mb-2 md:col-4 md:mb-0"
              >Display name</label
            >
            <div class="col-12 md:col-8">
              <input
                pinputtext=""
                id="name3"
                type="text"
                [(ngModel)]="user.userName"
                class="p-inputtext p-component p-element"
              />
            </div>
          </div>
          <div class="field grid">
            <label htmlfor="name3" class="col-12 mb-2 md:col-4 md:mb-0"
              >First name</label
            >
            <div class="col-12 md:col-8">
              <input
                pinputtext=""
                id="name3"
                type="text"
                class="p-inputtext p-component p-element"
                [(ngModel)]="user.firstName"
              />
            </div>
          </div>
          <div class="field grid">
            <label htmlfor="name3" class="col-12 mb-2 md:col-4 md:mb-0"
              >Last name</label
            >
            <div class="col-12 md:col-8">
              <input
                pinputtext=""
                id="name3"
                type="text"
                class="p-inputtext p-component p-element"
                name="surname"
                [(ngModel)]="user.lastName"
              />
            </div>
          </div>
          <div class="field grid">
            <label htmlfor="name3" class="col-12 mb-2 md:col-4 md:mb-0"
              >Password</label
            >
            <div class="col-12 md:col-8">
              <input
                pinputtext=""
                id="name3"
                type="text"
                class="p-inputtext p-component p-element"
                name="surname"
                [(ngModel)]="user.password"
              />
            </div>
          </div>
          <div class="field grid">
            <label htmlfor="name3" class="col-12 mb-2 md:col-4 md:mb-0"
              >Object ID</label
            >
            <div class="col-12 md:col-8">
              <input
                pinputtext=""
                id="name3"
                type="text"
                class="p-inputtext p-component p-element"
                [(ngModel)]="user.id"
              />
            </div>
          </div>
          <div class="field grid">
            <label htmlfor="name3" class="col-12 mb-2 md:col-4 md:mb-0"
              >Job title</label
            >
            <div class="col-12 md:col-8">
              <input
                pinputtext=""
                id="name3"
                type="text"
                class="p-inputtext p-component p-element"
                [(ngModel)]="user.jobTitle"
              />
            </div>
          </div>
          <div class="field grid">
            <label htmlfor="name3" class="col-12 mb-2 md:col-4 md:mb-0"
              >Company name</label
            >
            <div class="col-12 md:col-8">
              <input
                pinputtext=""
                id="name3"
                type="text"
                class="p-inputtext p-component p-element"
                [(ngModel)]="user.companyName"
              />
            </div>
          </div>
          <div class="field grid">
            <label htmlfor="name3" class="col-12 mb-2 md:col-4 md:mb-0"
              >Department</label
            >
            <div class="col-12 md:col-8">
              <input
                pinputtext=""
                id="name3"
                type="text"
                class="p-inputtext p-component p-element"
                [(ngModel)]="user.departement"
              />
            </div>
          </div>
          <div class="field grid">
            <label htmlfor="name3" class="col-12 mb-2 md:col-4 md:mb-0"
              >Employee type</label
            >
            <div class="col-12 md:col-8">
              <input
                pinputtext=""
                id="name3"
                type="text"
                class="p-inputtext p-component p-element"
                [(ngModel)]="user.employeeType"
              />
            </div>
          </div>
          <div class="field grid">
            <label htmlfor="name3" class="col-12 mb-2 md:col-4 md:mb-0"
              >Office location</label
            >
            <div class="col-12 md:col-8">
              <input
                pinputtext=""
                id="name3"
                type="text"
                class="p-inputtext p-component p-element"
                [(ngModel)]="user.officeLocation"
              />
            </div>
          </div>
          <div class="field grid">
            <label htmlfor="name3" class="col-12 mb-2 md:col-4 md:mb-0"
              >Street address</label
            >
            <div class="col-12 md:col-8">
              <input
                pinputtext=""
                id="name3"
                type="text"
                class="p-inputtext p-component p-element"
                [(ngModel)]="user.streetAddress"
              />
            </div>
          </div>
          <div class="field grid">
            <label htmlfor="name3" class="col-12 mb-2 md:col-4 md:mb-0"
              >City</label
            >
            <div class="col-12 md:col-8">
              <input
                pinputtext=""
                id="name3"
                type="text"
                class="p-inputtext p-component p-element"
                [(ngModel)]="user.city"
              />
            </div>
          </div>
          <div class="field grid">
            <label htmlfor="name3" class="col-12 mb-2 md:col-4 md:mb-0"
              >ZIP or postal code</label
            >
            <div class="col-12 md:col-8">
              <input
                pinputtext=""
                id="name3"
                type="text"
                class="p-inputtext p-component p-element"
                [(ngModel)]="user.postalCode"
              />
            </div>
          </div>
          <div class="field grid">
            <label htmlfor="name3" class="col-12 mb-2 md:col-4 md:mb-0"
              >Country or region</label
            >
            <div class="col-12 md:col-8">
              <input
                pinputtext=""
                id="name3"
                type="text"
                class="p-inputtext p-component p-element"
                [(ngModel)]="user.country"
              />
            </div>
          </div>
          <div class="field grid">
            <label htmlfor="name3" class="col-12 mb-2 md:col-4 md:mb-0"
              >Mobile phone</label
            >
            <div class="col-12 md:col-8">
              <input
                pinputtext=""
                id="name3"
                type="text"
                class="p-inputtext p-component p-element"
                [(ngModel)]="user.mobilePhone"
              />
            </div>
          </div>
          <div class="field grid">
            <label htmlfor="name3" class="col-12 mb-2 md:col-4 md:mb-0"
              >Email</label
            >
            <div class="col-12 md:col-8">
              <input
                pinputtext=""
                id="name3"
                type="text"
                class="p-inputtext p-component p-element"
                [(ngModel)]="user.mail"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  </ng-template>
</form-view>
  `,
})
export class UserEditComponent  {
  user: any;

}
