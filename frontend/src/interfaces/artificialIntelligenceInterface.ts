// content request
export interface IPart {
    text: string;
}

export interface IContent {
    parts: IPart[];
}

export interface IContentRequest {
    contents: IContent[];
}

// content response
export interface IPartResponse {
    text: string;
}

export interface IContentResponse {
    parts: IPartResponse[];
    role: string;
}

export interface ISafetyRating {
    category: string;
    probability: string;
}

export interface IPromptFeedback {
    safetyRatings: ISafetyRating[];
}

export interface ICandidate {
    content: IContent;
    finishSession: string;
    index: number;
    safetyRatings: ISafetyRating[];
}

export interface IContentResponseRoot {
    candidates: ICandidate[];
    promptFeedback: IPromptFeedback;
}


// prompt request
export interface IPromptRequest {
    prompt: string;
}

export interface ITranslateTextRequest {
    sourceLanguage: string;
    targetLanguage: string;
    text: string;
}