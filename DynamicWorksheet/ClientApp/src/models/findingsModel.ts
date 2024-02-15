export class FindingModel {
  findingId: number;
  findingTitle: string;
  findingOrder: number;
  statements: Statement[];

  constructor() {
    this.findingId = 0;
    this.findingTitle = '';
    this.findingOrder = 0;
    this.statements = [];
    }
}

export class Statement {
  statementId: number;
  description: string;
  fieldIds: SelectedStatementOption[];
  constructor() {
    this.statementId = 0;
    this.description = '';
    this.fieldIds = [];
  }

}



export class FindingStatementModel {
  findingId: number;
  findingTitle: string;
  findingOrder: number;

  constructor() {
    this.findingId = 0;
    this.findingTitle = '';
    this.findingOrder = 0;
  }
}

export class SelectedStatementOption {
  fieldId: number;
  fieldOptionId: number;

  constructor(FieldId, FieldOption) {
    this.fieldId = FieldId;
    this.fieldOptionId = FieldOption;
  }
}
