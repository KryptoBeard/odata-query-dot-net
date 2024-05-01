using System;
using System.Linq;

namespace OData.Query.Dot.Net
{
    using OData.Query.Dot.Net.Models;
    using OData.Query.Dot.Net.Types;
    using System.Collections.Generic;


    public class ODataQueryBuilder
    {

        public static string ITEM_ROOT = "";

        public static string BuildQuery<T>(PlainObject? filter = null, PlainObject? expand = null, PlainObject? orderBy = null, int? top = null, int? skip = null, bool count = false)
        {
            ITEM_ROOT = "";
            var builtFilter = BuildFilter<T>(filter);
            var builtExpand = BuildExpand<T>(expand);
            var builtOrderBy = BuildOrderBy<T>(orderBy);

            var buildObject = new PlainObject
            {
                ["$filter"] = builtFilter,
                ["$expand"] = builtExpand,
                ["$orderby"] = builtOrderBy
            };
            if (top != null)
            {
                buildObject.Add("$top", top);
            }
            if (skip != null)
            {
                buildObject.Add("$skip", skip);
            }
            if (count)
            {
                buildObject.Add("$count", true);
            }

            return BuildUrl("", buildObject);
        }

        private static string BuildFilter<T>(PlainObject filter = null, List<Alias> aliases = null, string propPrefix = "")
        {
            List<string> result = new List<string>();
            if (filter != null)
            {
                var builtFilter = BuildFilterCore<T>(filter, aliases, propPrefix);
                if (builtFilter != null)
                {
                    result.Add(builtFilter);
                }
            }
            return string.Join(" and ", result);
        }
        private static string BuildFilter<T>(Filter<T> filters = null, List<Alias> aliases = null, string propPrefix = "")
        {
            List<string> result = new List<string>();
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (filter != null)
                    {
                        var builtFilter = BuildFilterCore<T>(filter, aliases, propPrefix);
                        if (builtFilter != null)
                        {
                            result.Add(builtFilter);
                        }
                    }
                }
            }
            return string.Join(" and ", result);
        }

        private static string BuildFilterCore<T>(object filter = null, List<Alias> aliases = null, string propPrefix = "")
        {
            string filterExpr = "";
            if (filter is string)
            {
                filterExpr = (string)filter;
            }
            else if (filter is PlainObject)
            {
                var filtersArray = new List<string>();
                foreach (var filterKey in ((PlainObject)filter).Keys)
                {
                    var value = ((PlainObject)filter)[filterKey];
                    if (value == null)
                    {
                        continue;
                    }

                    string propName = "";
                    if (!string.IsNullOrEmpty(propPrefix))
                    {
                        if (filterKey == ITEM_ROOT)
                        {
                            propName = propPrefix;
                        }
                        else if (ODataRegex.IndexOfRegex.IsMatch(filterKey))
                        {
                            propName = ODataRegex.IndexOfRegex.Replace(filterKey, (match) =>
                            {
                                var y = match.Groups[1].Value.Trim();
                                return y == ITEM_ROOT ? $"({propPrefix})" : $"({propPrefix}/{match.Groups[1].Value.Trim()})";
                            });
                        }
                        else if (ODataRegex.FunctionRegex.IsMatch(filterKey))
                        {
                            propName = ODataRegex.FunctionRegex.Replace(filterKey, (match) =>
                            {
                                var x = match.Groups[1].Value.Trim();
                                return x == ITEM_ROOT ? $"({propPrefix})" : $"({propPrefix}/{match.Groups[1].Value.Trim()})";
                            });
                        }
                        else
                        {
                            propName = $"{propPrefix}/{filterKey}";
                        }
                    }
                    else
                    {
                        propName = filterKey;
                    }

                    if (filterKey == ITEM_ROOT && value is List<object>)
                    {
                        filtersArray.AddRange(((List<object>)value).Select(arrayValue => RenderPrimitiveValue(propName, arrayValue)));
                    }
                    else if (new List<string> { "number", "string", "boolean" }.Contains(value.GetType().Name.ToLower()) || value is DateTime || value == null)
                    {
                        filtersArray.Add(RenderPrimitiveValue(propName, value, aliases));
                    }
                    //else if (value is List<object>)
                    //{
                    //    var op = filterKey;
                    //    var builtFilters = ((List<object>)value)
                    //        .Select(v => BuildFilter<T>(v, aliases, propPrefix))
                    //        .Where(f => f != null)
                    //        .Select(f => ODataConstants.LogicalOperators.Contains(op) ? $"({f})" : f);
                    //    if (builtFilters.Any())
                    //    {
                    //        if (ODataConstants.LogicalOperators.Contains(op))
                    //        {
                    //            if (builtFilters.Any())
                    //            {
                    //                if (op == "not")
                    //                {
                    //                    filtersArray.Add(ParseNot(builtFilters.ToList()));
                    //                }
                    //                else
                    //                {
                    //                    filtersArray.Add($"({string.Join($" {op} ", builtFilters)})");
                    //                }
                    //            }
                    //        }
                    //        else
                    //        {
                    //            filtersArray.Add(string.Join($" {op} ", builtFilters));
                    //        }
                    //    }
                    //}
                    else if (ODataConstants.LogicalOperators.Contains(propName))
                    {
                        var op = propName;
                        var builtFilters = ((PlainObject)value).Keys.Select(valueKey => BuildFilterCore<T>(new PlainObject { [valueKey] = ((PlainObject)value)[valueKey] })).ToList();
                        if (builtFilters.Any())
                        {
                            if (op == "not")
                            {
                                filtersArray.Add(ParseNot(builtFilters));
                            }
                            else
                            {
                                filtersArray.Add($"({string.Join($" {op} ", builtFilters)})");
                            }
                        }
                    }
                    else if (value is PlainObject)
                    {
                        if (((PlainObject)value).ContainsKey("type"))
                        {
                            filtersArray.Add(RenderPrimitiveValue(propName, value, aliases));
                        }
                        else
                        {
                            var operators = ((PlainObject)value).Keys.ToList();
                            foreach (var op in operators)
                            {
                                if (((PlainObject)value)[op] == null)
                                {
                                    continue;
                                }

                                if (ODataConstants.ComparisonOperators.Contains(op))
                                {
                                    filtersArray.Add($"{propName} {op} {HandleValue(((PlainObject)value)[op], aliases)}");
                                }
                                else if (ODataConstants.LogicalOperators.Contains(op))
                                {
                                    if (((PlainObject)value)[op] is List<object>)
                                    {
                                        filtersArray.Add(string.Join($" {op} ", ((List<object>)((PlainObject)value)[op]).Select(v => $"({BuildFilterCore<T>(v, aliases, propName)})")));
                                    }
                                    else
                                    {
                                        filtersArray.Add($"({BuildFilterCore<T>(((PlainObject)value)[op], aliases, propName)})");
                                    }
                                }
                                else if (ODataConstants.CollectionOperators.Contains(op))
                                {
                                    var collectionClause = BuildCollectionClause(filterKey.ToLower(), ((PlainObject)value)[op], op, propName);
                                    if (collectionClause != null)
                                    {
                                        filtersArray.Add(collectionClause);
                                    }
                                }
                                else if (op == "has")
                                {
                                    filtersArray.Add($"{propName} {op} {HandleValue(((PlainObject)value)[op], aliases)}");
                                }
                                //else if (op == "in")
                                //{
                                //    var resultingValues = ((PlainObject)value)[op] is List<object> ? (List<object>)((PlainObject)value)[op] : ((PlainObject)((PlainObject)value)[op])["value"].Select(typedValue => new PlainObject { ["type"] = ((PlainObject)((PlainObject)value)[op])["type"], ["value"] = typedValue }).ToList();
                                //    filtersArray.Add($"{propName} in ({string.Join(",", resultingValues.Select(v => HandleValue(v, aliases)))})");
                                //}
                                else if (ODataConstants.BooleanFunctions.Contains(op))
                                {
                                    filtersArray.Add($"{op}({propName},{HandleValue(((PlainObject)value)[op], aliases)})");
                                }
                                else
                                {
                                    var filter2 = BuildFilterCore<T>((PlainObject)value, aliases, propName);
                                    if (filter2 != null)
                                    {
                                        filtersArray.Add(filter2);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Exception($"Unexpected value type: {value}");
                    }
                }

                filterExpr = string.Join(" and ", filtersArray);
            }
            return filterExpr;
        }
        private static string BuildExpand<T>(object expands)
        {
            if (expands is int)
            {
                return expands.ToString();
            }
            else if (expands is string)
            {
                if (((string)expands).IndexOf('/') == -1)
                {
                    return (string)expands;
                }

                return ((string)expands)
                    .Split('/')
                    .Reverse()
                    .Aggregate("", (results, item) =>
                    {
                        if (results == "")
                        {
                            return $"$expand={item}";
                        }
                        else
                        {
                            return $"{item}({results})";
                        }
                    });

            }
            else if (expands is List<NestedExpandOptions<object>>)
            {
                return string.Join(",", ((List<NestedExpandOptions<object>>)expands).Select(e => BuildExpand<T>(e)));
            }
            else if (expands is PlainObject)
            {
                var expandKeys = ((PlainObject)expands).Keys.ToList();
                if (expandKeys.Any(key => ODataConstants.SupportedExpandProperties.Contains(key.ToLower())))
                {
                    return string.Join(";", expandKeys.Select(key =>
                    {
                        object value;
                        switch (key)
                        {
                            case "filter":
                                value = BuildFilter(((PlainObject)expands)[key] as Filter<T>);
                                break;
                            case "orderBy":
                                value = BuildOrderBy<T>(((PlainObject)expands)[key] as OrderBy<T>);
                                break;
                            case "levels":
                            case "count":
                            case "skip":
                            case "top":
                                value = ((PlainObject)expands)[key].ToString();
                                break;
                            default:
                                value = BuildExpand<T>(((PlainObject)expands)[key]);
                                break;
                        }
                        return $"${key.ToLower()}={value}";
                    }));
                }
                else
                {
                    return string.Join(",", expandKeys.Select(key =>
                    {
                        var builtExpand = BuildExpand<T>(((PlainObject)expands)[key]);
                        return !string.IsNullOrEmpty(builtExpand) ? $"{key}({builtExpand})" : key;
                    }));
                }
            }
            return "";
        }

        private static string BuildOrderBy<T>(object orderBy, string prefix = "")
        {
            if (orderBy is List<OrderByOptions<T>>)
            {
                return string.Join(",", ((List<OrderByOptions<T>>)orderBy).Select(value =>
                {
                    if (value is List<string> && ((List<string>)value).Count == 2 && new List<string> { "asc", "desc" }.Contains(((List<string>)value)[1]))
                    {
                        return string.Join(" ", ((List<string>)value));
                    }
                    else
                    {
                        return $"{prefix}{value}";
                    }
                }));
            }
            else if (orderBy is PlainObject)
            {
                return string.Join(",", ((PlainObject)orderBy).Select(kvp => BuildOrderBy<T>(kvp.Value as OrderBy<T>, $"{kvp.Key}/")).Select(v => $"{prefix}{v}"));
            }
            return $"{prefix}{orderBy}";
        }

        private static string BuildUrl(string path, PlainObject parameters)
        {
            var queries = parameters.Where(kvp => kvp.Value != null && kvp.Value.ToString() != "").Select(kvp => $"{kvp.Key}={kvp.Value}");
            return queries.Any() ? $"{path}?{string.Join("&", queries)}" : path;
        }

        public static string ParseNot(List<string> builtFilters)
        {
            return $"not ({string.Join(" and ", builtFilters)})";
        }

        private static string RenderPrimitiveValue(string propName, object value, List<Alias> aliases = null)
        {
            if (value is string)
            {
                return $"{propName} eq '{value}'";
            }
            else if (value is DateTime)
            {
                return $"{propName} eq {((DateTime)value).ToString("yyyy-MM-ddTHH:mm:ssZ")}";
            }
            else if (value is bool)
            {
                return $"{propName} eq {value.ToString().ToLower()}";
            }
            else if (value is Raw)
            {
                return $"{propName} eq {((Raw)value).Value}";
            }
            else if (value is Guid)
            {
                return $"{propName} eq {((Guid)value).Value}";
            }
            else if (value is Duration)
            {
                return $"{propName} eq {((Duration)value).Value}";
            }
            else if (value is Binary)
            {
                return $"{propName} eq {((Binary)value).Value}";
            }
            else if (value is Json)
            {
                return $"{propName} eq {((Json)value).Value}";
            }
            else if (value is Alias)
            {
                var alias = (Alias)value;
                aliases.Add(alias);
                return $"{propName} eq @{alias.Value}";
            }
            else if (value is Decimal)
            {
                return $"{propName} eq {((Decimal)value).Value}";
            }
            return "";
        }

        private static string HandleValue(object value, List<Alias> aliases = null)
        {
            if (value is string)
            {
                return $"'{value}'";
            }
            else if (value is DateTime)
            {
                return $"'{((DateTime)value).ToString("yyyy-MM-ddTHH:mm:ssZ")}'";
            }
            else if (value is bool)
            {
                return value.ToString().ToLower();
            }
            else if (value is Raw)
            {
                return ((Raw)value).Value.ToString();
            }
            else if (value is Guid)
            {
                return ((Guid)value).Value.ToString();
            }
            else if (value is Duration)
            {
                return ((Duration)value).Value.ToString();
            }
            else if (value is Binary)
            {
                return ((Binary)value).Value.ToString();
            }
            else if (value is Json)
            {
                return ((Json)value).Value.ToString();
            }
            else if (value is Alias)
            {
                var alias = (Alias)value;
                aliases.Add(alias);
                return $"@{alias.Value}";
            }
            else if (value is Decimal)
            {
                return ((Decimal)value).Value.ToString();
            }
            return "";
        }

        private static string BuildCollectionClause(string filterKey, object value, string op, string propName)
        {
            if (value is List<object>)
            {
                var collectionClause = BuildFilter((Filter<object>)value, null, propName);
                if (collectionClause != null)
                {
                    return $"{propName}/{filterKey}({collectionClause})";
                }
            }
            else if (value is PlainObject)
            {
                var collectionClause = BuildFilter((Filter<object>)((PlainObject)value)["value"], null, propName);
                if (collectionClause != null)
                {
                    return $"{propName}/{filterKey}({collectionClause})";
                }
            }
            return null;
        }
    }
}

