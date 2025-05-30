export interface Propertyfilter {
    propertyText: string;
    propertyName: string;
    operator: Operator;
    propertyValue: string;
    propertyType : string;
}

export interface Filter {
    keyword: string;
    operator: LogicalOperator;
    propertyfilters: Propertyfilter[];
}

export enum LogicalOperator {
    And,
    Or
}

export enum Operator {
    Equal,
    NotEqual,
    Contains,
    GreaterThan,
    GreaterThanOrEqual,
    LessThanOrEqual,
    LessThan
}