import { Component, OnInit } from '@angular/core';
import { UserAzureAdModel } from 'src/app/models/user.model';
import { UserService } from 'src/app/services/user.service';

@Component({
  templateUrl: "./user-form.component.html",
})
export class UserMeComponent implements OnInit {
  me!: UserAzureAdModel;
  imageUrl! : any;

  /**
   *
   */
  constructor(
    private userService: UserService
  ) {}
  ngOnInit(): void {
    this.userService.me().subscribe((me) => {
      this.me = me;
    });

    this.userService.mePhoto().subscribe((photo) => {
      const reader = new FileReader();
      reader.onload = (e) => this.imageUrl = e?.target?.result ??'';
      reader.readAsDataURL(photo);
    });
  }
}
