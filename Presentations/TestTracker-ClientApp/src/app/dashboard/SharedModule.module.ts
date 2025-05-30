import { NgModule } from '@angular/core';
import { HasPermissionDirective } from '../directive/PermissionDirective';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@NgModule({
  declarations: [HasPermissionDirective],
  imports: [TranslateModule], // Ajoutez TranslateModule dans les imports
  exports: [HasPermissionDirective, TranslateModule], // Exportez TranslateModule pour partager les services de traduction
})

export class SharedModule {
  constructor(private translate: TranslateService) {
    translate.setDefaultLang('fr');
  }

  switchLanguage() {
    this.translate.use(this.translate.currentLang === 'fr' ? 'en' : 'fr');
  }
}
