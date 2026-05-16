import 'package:turisticka_agencija_admin/models/Recommender.dart';

import 'base_provider.dart';

class RecommenderProvider extends BaseProvider<Recommender> {
  RecommenderProvider() : super("api/Recommender/GetRecommendedPutovanja");

  @override
  Recommender fromJson(data) {
    return Recommender.fromJson(data);
  }
}
