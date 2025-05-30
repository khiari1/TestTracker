import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ThemeService {
  private lightTheme: string = 'light';

  constructor() {}

    /**
     * Set the theme for the application.
     * @param theme - The name of the theme to set (e.g., 'light', 'dark').
     */

  dark(): void {
    document.body.classList.remove(this.lightTheme);
  }

  light(): void {
    document.body.classList.add(this.lightTheme);
  }

  darkLightMode(): void {
    if (document.body.classList.contains(this.lightTheme)) {
      this.dark();
    } else {
      this.light();
    }
  }
}