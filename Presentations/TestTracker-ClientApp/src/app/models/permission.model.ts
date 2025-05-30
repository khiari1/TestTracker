export class PermissionModel {
  key: string | undefined;
  value: string | undefined;
  selected?: boolean;
}

export interface Permission {
  key: string | undefined;
  name: string;
  description: string;
}
