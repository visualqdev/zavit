import * as HtmlEncoder from "./htmlEncoder";

// Tagged template function
export function html(pieces) {
    var result = pieces[0];
    const substitutions = [].slice.call(arguments, 1);
    for (let i = 0; i < substitutions.length; ++i) {
        result += HtmlEncoder.htmlEncode(substitutions[i]) + pieces[i + 1];
    }

    return result;
}